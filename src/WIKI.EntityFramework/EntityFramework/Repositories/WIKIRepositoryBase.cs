using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace WIKI.EntityFramework.Repositories
{
    public abstract class WIKIRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<WIKIDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected WIKIRepositoryBase(IDbContextProvider<WIKIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class WIKIRepositoryBase<TEntity> : WIKIRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected WIKIRepositoryBase(IDbContextProvider<WIKIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
