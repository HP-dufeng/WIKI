using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using WIKI.Core;
using WIKI.Core.Entities;
using WIKI.EntityFramework;
using WIKI.WebApi.Models;

namespace WIKI.WebApi.Controllers
{
    public class ContentController : ODataController
    {
        [HttpPost]
        //[EnableQuery(AllowedArithmeticOperators = System.Web.OData.Query.AllowedArithmeticOperators.All, AllowedLogicalOperators = AllowedLogicalOperators.All, MaxNodeCount = 1000)]
        public IHttpActionResult SearchQuestion(ODataQueryOptions<Content> queryOptions)
        {
            var Db = new WIKIDbContext();

            var contentType = ContentTypeEnum.Question.ToString();

            var query = from q in queryOptions.ApplyTo(Db.Content).Cast<Content>()
                        join d in Db.Question on q.Id equals d.Id
                        from a in Db.Account.Where(m=>m.UserName == d.CreatedBy).DefaultIfEmpty()
                        where q.ContentType == contentType
                        select new
                        {
                            Question = d,
                            ExpandProperty = new ExpandPropertyDto
                            {
                                CreatedBy = a.FullName,
                                CreatedByDepartment = a.Department
                            },
                            Tags = Db.Tag.Where(m => m.ContentId == d.Id).Select(m => m.Value)
                        };

            var result = query.ToList().Select(m =>
            {
                var dto = m.Question.MapToDto();
                dto.ExpandProperty = m.ExpandProperty;
                return new
                {
                    Question = dto,
                    Tags = m.Tags
                };
            });



            long? count = 0;
            if (queryOptions.Count != null)
                count = queryOptions.Request.ODataProperties().TotalCount;

            return Json(new { Datas = result, Count = count });

        }

        [HttpPost]
        //[EnableQuery(AllowedArithmeticOperators = System.Web.OData.Query.AllowedArithmeticOperators.All, AllowedLogicalOperators = AllowedLogicalOperators.All, MaxNodeCount = 1000)]
        public IHttpActionResult SearchDocument(ODataQueryOptions<Content> queryOptions)
        {
            var Db = new WIKIDbContext();

            var contentType = ContentTypeEnum.Document.ToString();

            var query = from q in queryOptions.ApplyTo(Db.Content).Cast<Content>()
                        join d in Db.Document on q.Id equals d.Id
                        from a in Db.Account.Where(m => m.UserName == d.CreatedBy).DefaultIfEmpty()
                        where q.ContentType == contentType
                        select new
                        {
                            Document = d,
                            ExpandProperty = new ExpandPropertyDto
                            {
                                CreatedBy = a.FullName,
                                CreatedByDepartment = a.Department
                            },
                            Tags = Db.Tag.Where(m => m.ContentId == d.Id).Select(m => m.Value)
                        };

            var result = query.ToList().Select(m =>
            {
                var dto = m.Document.MapToDto();
                dto.ExpandProperty = m.ExpandProperty;
                return new
                {
                    Document = dto,
                    Tags = m.Tags
                };
            });


            long? count = 0;
            if (queryOptions.Count != null)
                count = queryOptions.Request.ODataProperties().TotalCount;

            return Json(new { Datas = result, Count = count });

        }

    }
}