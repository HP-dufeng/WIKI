using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using WIKI.Core.Entities;
using WIKI.EntityFramework;
using WIKI.WebApi.Models;

namespace WIKI.WebApi.Controllers
{
    public class ViewLogDailyController : ODataController
    {
        private WIKIDbContext Db = new WIKIDbContext();

        [HttpPost]
        public IHttpActionResult Create(ODataActionParameters parameters)
        {
            if (parameters["dto"] == null)
                throw new Exception("输入参数错误");

            var dto = parameters["dto"] as ViewLogDailyCreateInputDto;

            this.Validate(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = dto.MapToEntity();

            Db.ViewLogDaily.Add(entity);
            Db.SaveChanges();
            
            
            return Created(entity);
        }

    }
}