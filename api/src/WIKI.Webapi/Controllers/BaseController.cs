using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIKI.Data;
using WIKI.EntityFramework;
using WIKI.OData;

namespace WIKI.WebApi.Controllers
{
    public class BaseController<TEntity> : ODataBaseController<WIKIDbContext, TEntity, long>
    where TEntity : Entity<long>
    {
        public BaseController()
            : base(new WIKIDbContext())
        {

        }
    }
}