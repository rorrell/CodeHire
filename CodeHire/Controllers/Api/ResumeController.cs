using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using CodeHire.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeHire.Controllers.Api
{
    public class ResumeController : ApiController
    {
        private ResumeBusinessLogic bll;

        public ResumeController()
        {
            bll = new ResumeBusinessLogic(new ApplicationDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                bll.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetResumeByUser(string userId)
        {
            return Ok(bll.GetByUser(userId));
        }

        public IHttpActionResult GetResumeById(int id)
        {
            return Ok(bll.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult CreateResume(ResumeDto resumeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var createdResume = bll.Create(resumeDto, User.Identity.GetUserId());
            return Created(new Uri(Request.RequestUri + "/" + createdResume.Id), createdResume);
        }

        [HttpPut]
        public IHttpActionResult UpdateResume(int id, ResumeDto resumeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var success = bll.Update(id, resumeDto);

            if (!success)
                return NotFound();

            return Ok();
        }

        [Route("api/resume/skills/{id}")]
        public IHttpActionResult GetResumeSkills(int id)
        {
            var resume = bll.GetById(id);

            return Ok(resume.Skills);
        }

        [HttpPost]
        [Route("api/resume/skills")]
        public IHttpActionResult UpdateResumeSkills(ResumeSkillsDto resumeSkillsDto)
        {
            var resume = bll.GetById(resumeSkillsDto.ResumeId);
            var success = bll.Update(resume.Id, resume, resumeSkillsDto.SkillIds);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("api/resume/skills/{id}/{resumeId}")]
        public IHttpActionResult DeleteResumeSkill(int id, int resumeId)
        {
            var success = bll.DeleteSkill(resumeId, id);

            if (!success)
                return BadRequest();

            return Ok();
        }
    }
}
