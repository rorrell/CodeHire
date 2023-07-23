using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using CodeHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeHire.Controllers.Api
{
    public class SkillsController : ApiController
    {
        private SkillsBusinessLogic bll;

        public SkillsController()
        {
            bll = new SkillsBusinessLogic(new ApplicationDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                bll.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetSkills(string query = null)
        {
            return Ok(bll.GetAll(query));
        }

        [HttpPost]
        public IHttpActionResult CreateSkill(SkillDto skillDto)
        {
            if (ModelState.IsValid)
                return BadRequest();

            var createdSkill = bll.Create(skillDto);

            return Created(new Uri(Request.RequestUri + "/" + createdSkill.Id), createdSkill);
        }
    }
}
