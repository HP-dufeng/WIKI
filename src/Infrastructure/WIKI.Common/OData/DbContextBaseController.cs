using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;

namespace WIKI.OData
{
    public class DbContextBaseController<TDbContext> : ODataController
    {
        private TDbContext _db;
        public TDbContext Db
        {
            get
            {
                return _db;
            }
        }

        public DbContextBaseController(TDbContext db)
        {
            _db = db;
        }
    }
}