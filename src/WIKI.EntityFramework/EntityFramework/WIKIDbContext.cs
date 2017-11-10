using System.Data.Common;
using Abp.EntityFramework;
using System.Data.Entity;
using WIKI.Users;

namespace WIKI.EntityFramework
{
    public class WIKIDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Tags.Tag> Tag { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public WIKIDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in WIKIDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of WIKIDbContext since ABP automatically handles it.
         */
        public WIKIDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public WIKIDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public WIKIDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
