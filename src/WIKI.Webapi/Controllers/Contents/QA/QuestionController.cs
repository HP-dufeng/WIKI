using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using WIKI.Core.Entities;
using WIKI.EntityFramework;
using WIKI.WebApi.Models;

namespace WIKI.WebApi.Controllers
{
    public class QuestionController : ODataController
    {
        private WIKIDbContext Db = new WIKIDbContext();


        public IHttpActionResult Get([FromODataUri] long key)
        {
            var queryQuestion = (from q in Db.Question
                        from cb in Db.Account.Where(m=>m.UserName == q.CreatedBy).DefaultIfEmpty()
                        where q.Id == key
                        select new
                        {
                            Question = q,
                            ExpandProperty = new ExpandPropertyDto
                            {
                                CreatedBy = cb.FullName,
                                CreatedByDepartment = cb.Department
                            }
                        }).FirstOrDefault();

            if (queryQuestion == null)
                return NotFound();

            var questionDto = queryQuestion.Question.MapToDto();
            questionDto.ExpandProperty = queryQuestion.ExpandProperty;

            var queryAnswers = (from an in Db.Answer
                                from cb in Db.Account.Where(m => m.UserName == an.CreatedBy).DefaultIfEmpty()
                                where an.QuestionId == key
                                select new
                                {
                                    Answer = an,
                                    ExpandProperty = new ExpandPropertyDto
                                    {
                                        CreatedBy = cb.FullName,
                                        CreatedByDepartment = cb.Department
                                    }
                                }).ToList();

            var result = new QuestionGetOutputDto
            {
                Question = questionDto,
                Answers = queryAnswers.Select(m => 
                {
                    var dto = m.Answer.MapToDto();
                    dto.ExpandProperty = m.ExpandProperty;
                    return dto;
                }).ToList(),
                Tags = Db.Tag.Where(m => m.ContentId == key ).Select(m => m.Value).ToList(),
                ViewCount = Db.ViewLogDaily.Count(m=>m.ContentId == key)
            };

            return Json(result);
        }

        [HttpPost]
        public IHttpActionResult Create(ODataActionParameters parameters)
        {
            if (parameters["dto"] == null)
                throw new Exception("输入参数错误");

            var dto = parameters["dto"] as QuestionCreateInputDto;

            this.Validate(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionEntity = dto.MapToEntity();
            questionEntity.CreatedBy = User.Identity.Name;
            foreach (var item in dto.Answers)
            {
                questionEntity.Answers.Add(new Answer { Text = item, CreatedBy = User.Identity.Name });
            }

            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var contentEntity = questionEntity.MapToContent();
                    Db.Content.Add(contentEntity);
                    Db.SaveChanges();

                    questionEntity.Id = contentEntity.Id;
                    Db.Question.Add(questionEntity);

                    foreach (var t in dto.Tags)
                    {
                        var tag = new Tag
                        {
                            ContentId = questionEntity.Id,
                            Value = t
                        };
                        Db.Tag.Add(tag);
                    }

                    Db.SaveChanges();

                    transaction.Commit();

                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            

            return Created(questionEntity);
        }

        [HttpPost]
        public IHttpActionResult Update([FromODataUri] long key, ODataActionParameters parameters)
        {
            if (parameters["dto"] == null)
                throw new Exception("输入参数错误");

            var dto = parameters["dto"] as QuestionUpdateInputDto;
            this.Validate(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var questionEntity = Db.Question.Find(key);
            if (questionEntity == null)
                return NotFound();

            using (var transaction = Db.Database.BeginTransaction())
            {
                questionEntity.Title = dto.Title;
                questionEntity.Important = dto.Important;
                questionEntity.Sticky = dto.Sticky;
                questionEntity.Text = dto.Text;
                questionEntity.UpdatedTime = DateTime.Now;
                questionEntity.UpdatedBy = User.Identity.Name;

                var contentEntity = questionEntity.MapToContent();
                Db.Content.Attach(contentEntity);
                Db.Entry(contentEntity).State = System.Data.Entity.EntityState.Modified;

                foreach (var item in Db.Tag.Where(m => m.ContentId == key))
                    Db.Tag.Remove(item);

                foreach (var item in dto.Tags)
                {
                    Db.Tag.Add(new Tag
                    {
                        ContentId = questionEntity.Id,
                        Value = item,
                    });
                }

                try
                {
                    Db.SaveChanges();
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return Updated(questionEntity);
        }

        public virtual IHttpActionResult Delete([FromODataUri] long key)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {

                foreach (var tag in Db.Tag.Where(m => m.ContentId == key))
                    Db.Tag.Remove(tag);

                var questionEntity = Db.Question.Find(key);
                if (questionEntity != null)
                    Db.Question.Remove(questionEntity);

                var contentEntity = Db.Content.Find(key);
                if (contentEntity != null)
                    Db.Content.Remove(contentEntity);

                try
                {
                    Db.SaveChanges();
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return Json(new { });
        }

    }
}