using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WIKI.Core.Entities;
using WIKI.EntityFramework;

namespace WIKI.WebApi.Controllers.Documents
{
    public class DocumentViewController : ODataController
    {

        [EnableQuery(AllowedArithmeticOperators = System.Web.OData.Query.AllowedArithmeticOperators.All, AllowedLogicalOperators = AllowedLogicalOperators.All, MaxNodeCount = 1000)]
        public IHttpActionResult Get(ODataQueryOptions<DocumentView> queryOptions)
        {
            var Db = new WIKIDbContext();

            return Ok<IEnumerable<DocumentView>>(Db.Set<DocumentView>().AsQueryable());

        }

    }
}