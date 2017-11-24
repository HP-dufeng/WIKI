using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using WIKI.Core.Entities;
using WIKI.EntityFramework;
using WIKI.WebApi.Models;

namespace WIKI.WebApi.Controllers.Documents
{
    public class DocumentController : ODataController
    {
        private WIKIDbContext Db = new WIKIDbContext();


        public IHttpActionResult Get([FromODataUri] long key)
        {
            var entity = (from d in Db.Document
                          from a in Db.Account.Where(m => m.UserName == d.CreatedBy).DefaultIfEmpty()
                          where d.Id == key
                          select new
                          {
                              Document = d,
                              ExpandProperty = new ExpandPropertyDto
                              {
                                  CreatedBy = a.FullName,
                                  CreatedByDepartment = a.Department
                              }
                          }).FirstOrDefault();

            if (entity == null)
                return NotFound();

            var dto = entity.Document.MapToDto();
            dto.ExpandProperty = entity.ExpandProperty;

            var attachments = new List<DocumentAttachmentDto>();
            if(entity.Document.DocumentAttachmentList != null)
            {
                attachments = entity.Document.DocumentAttachmentList.Select(m =>
                {
                    var attachmentDto = m.MapToDto();
                    attachmentDto.DownLoadCount = Db.DownLoadLogDaily.Count(d => d.AttachmentId == m.Id);
                    return attachmentDto;
                }).ToList();
            }

            var result = new DocumentGetOutputDto
            {
                Document = dto,
                Attachments = attachments,
                Tags = Db.Tag.Where(m => m.ContentId == key).Select(m => m.Value).ToList(),
                ViewCount = Db.ViewLogDaily.Count(m=>m.ContentId == key)
            };

            return Json(result);
        }

        [HttpPost]
        public IHttpActionResult Create(ODataActionParameters parameters)
        {
            if (parameters["dto"] == null)
                throw new Exception("输入参数错误");

            var dto = parameters["dto"] as DocumentCreateInputDto;

            this.Validate(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentEntity = dto.MapToEntity();
            documentEntity.CreatedBy = User.Identity.Name;


            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var contentEntity = documentEntity.MapToContent();
                    Db.Content.Add(contentEntity);
                    Db.SaveChanges();

                    documentEntity.Id = contentEntity.Id;
                    Db.Document.Add(documentEntity);

                    foreach (var t in dto.Tags)
                    {
                        var tag = new Tag
                        {
                            ContentId = documentEntity.Id,
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
            return Created(documentEntity);
        }

        [HttpPost]
        public IHttpActionResult Update([FromODataUri] long key, ODataActionParameters parameters)
        {
            if (parameters["dto"] == null)
                throw new Exception("输入参数错误");

            var dto = parameters["dto"] as DocumentUpdateInputDto;
            this.Validate(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var documentEntity = Db.Document.Find(key);
            if (documentEntity == null)
                return NotFound();

            documentEntity.Title = dto.Title;
            documentEntity.Important = dto.Important;
            documentEntity.Sticky = dto.Sticky;
            documentEntity.Description = dto.Description;
            documentEntity.UpdatedTime = DateTime.Now;
            documentEntity.UpdatedBy = User.Identity.Name;

            using (var transaction = Db.Database.BeginTransaction())
            {
                var contentEntity = documentEntity.MapToContent();
                Db.Content.Attach(contentEntity);
                Db.Entry(contentEntity).State = System.Data.Entity.EntityState.Modified;

                foreach (var item in Db.Tag.Where(m => m.ContentId == key))
                    Db.Tag.Remove(item);

                foreach (var item in dto.Tags)
                {
                    Db.Tag.Add(new Tag
                    {
                        ContentId = documentEntity.Id,
                        Value = item,
                    });
                }

                try
                {
                    Db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return Updated(documentEntity);
        }


        public virtual IHttpActionResult Delete([FromODataUri] long key)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {

                foreach (var tag in Db.Tag.Where(m => m.ContentId == key))
                    Db.Tag.Remove(tag);

                var documentEntity = Db.Document.Find(key);
                if (documentEntity != null)
                    Db.Document.Remove(documentEntity);

                var contentEntity = Db.Content.Find(key);
                if (contentEntity != null)
                    Db.Content.Remove(contentEntity);

                try
                {
                    Db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return Json(new { });
        }
    }
}