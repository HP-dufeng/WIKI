
using CommonLibs.Common;
using Microsoft.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WIKI.Data;
using WIKI.Mapper;

namespace WIKI.OData
{
    public class ODataBaseController<TDbContext, TEntity, TKey> : DbContextBaseController<TDbContext>
        where TDbContext : DbContext, new()
        where TEntity : Entity<TKey>
    {
        public ODataBaseController(TDbContext db)
            : base(db)
        {

        }

        protected static ODataValidationSettings _validationSettings = new ODataValidationSettings()
        {
            AllowedLogicalOperators = AllowedLogicalOperators.All,
            AllowedArithmeticOperators = AllowedArithmeticOperators.All,
            AllowedQueryOptions = AllowedQueryOptions.All,
            MaxTop = AppSetting.GetInt32("MaxTop"),
            MaxNodeCount = 1000
        };

        [EnableQuery(AllowedArithmeticOperators = System.Web.OData.Query.AllowedArithmeticOperators.All, AllowedLogicalOperators = AllowedLogicalOperators.All, MaxNodeCount = 1000)]
        public virtual IHttpActionResult Get(ODataQueryOptions<TEntity> queryOptions)
        {
            // validate the query.      

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok<IEnumerable<TEntity>>(Db.Set<TEntity>().AsQueryable());
        }

        // GET: odata/TEntitys(5)
        [EnableQuery(AllowedArithmeticOperators = System.Web.OData.Query.AllowedArithmeticOperators.All, MaxNodeCount = 1000)]
        public virtual IHttpActionResult Get([FromODataUri] TKey key, ODataQueryOptions<TEntity> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(SingleResult.Create<TEntity>(Db.Set<TEntity>().Where(t => t.Id.ToString() == key.ToString())));
        }

        // PUT: odata/TEntitys(5)
        public virtual IHttpActionResult Put([FromODataUri] TKey key, Delta<TEntity> delta)
        {
            Validate(delta);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Db.Set<TEntity>().Find(key);
            //var createdTime = entity.CreatedTime;
            //var createdBy = entity.CreatedBy;

            delta.Put(entity);
            entity.Id = key;
            //entity.CreatedTime = createdTime;
            //entity.CreatedBy = createdBy;
            //entity.UpdatedTime = DateTime.Now;
            //entity.UpdatedBy = User.Identity.Name;

            Db.SaveChanges();

            return Updated(entity);
        }

        // POST: odata/TEntitys
        public virtual IHttpActionResult Post(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //entity.CreatedTime = DateTime.Now;
            //entity.CreatedBy = User.Identity.Name;
            Db.Set<TEntity>().Add(entity);
            try
            {
                Db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Created(entity);
        }

        // PATCH: odata/TEntitys(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual IHttpActionResult Patch([FromODataUri] TKey key, Delta<TEntity> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbEntity = Db.Set<TEntity>().Find(key);

            var entity = AutoMapperHelper<TEntity, TEntity>.Mapper.Map<TEntity>(dbEntity);
            delta.Patch(entity);
            entity.Id = key;
            //entity.UpdatedTime = DateTime.Now;
            //entity.UpdatedBy = User.Identity.Name;

            dbEntity = AutoMapperHelper<TEntity, TEntity>.Mapper.Map<TEntity>(entity);

            if (Db.Entry(dbEntity).State == EntityState.Modified)
            {
                Db.SaveChanges();
            }
            else if (Db.Entry(dbEntity).State == EntityState.Detached)
            {
                try
                {
                    Db.Set<TEntity>().Attach(dbEntity);
                    Db.Entry(dbEntity).State = EntityState.Modified;
                }
                catch (InvalidOperationException)
                {
                    TEntity old = Db.Set<TEntity>().Find(key);
                    Db.Entry(old).CurrentValues.SetValues(dbEntity);
                }
                Db.SaveChanges();
            }

            return Updated(entity);
        }

        // DELETE: odata/TEntitys(5)
        public virtual IHttpActionResult Delete([FromODataUri] TKey key)
        {
            var entity = Db.Set<TEntity>().Find(key);
            Db.Set<TEntity>().Remove(entity);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
