using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using WIKI.Core.Entities;

namespace WIKI.WebApi.Controllers.QA
{
    public class AnswerController : BaseController<Answer>
    {
        public override IHttpActionResult Post(Answer entity)
        {
            entity.CreatedBy = User.Identity.Name;

            return base.Post(entity);
        }

        public override IHttpActionResult Patch([FromODataUri] long key, Delta<Answer> delta)
        {
            delta.TrySetPropertyValue("UpdatedTime", DateTime.Now);
            delta.TrySetPropertyValue("UpdatedBy", User.Identity.Name);

            return base.Patch(key, delta);
        }
    }
}