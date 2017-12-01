using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WIKI.Core.Entities;
using WIKI.EntityFramework;

namespace WIKI.WebApi.Controllers.Tags
{
    public class TagController : ODataController
    {
        [HttpPost]
        //[EnableQuery(AllowedArithmeticOperators = System.Web.OData.Query.AllowedArithmeticOperators.All, AllowedLogicalOperators = AllowedLogicalOperators.All, MaxNodeCount = 1000)]
        public IHttpActionResult Search(ODataQueryOptions<Tag> queryOptions)
        {
            var Db = new WIKIDbContext();

            var query = queryOptions.ApplyTo(Db.Tag).Cast<Tag>()
                            .Select(m => m.Value)
                            .Distinct();



            return Json(query.ToList());

        }
    }

    
}