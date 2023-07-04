using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeHire.Models;
using System.Data.Entity;
using AutoMapper;
using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using Microsoft.AspNet.Identity;

namespace CodeHire.Controllers.Api
{
    public class JobListingsController : ApiController
    {
        private JobListingsBusinessLogic bll;

        public JobListingsController()
        {
            bll = new JobListingsBusinessLogic(new ApplicationDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                bll.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetJobListings()
        {
            return Ok(bll.GetAll());
        }

        public IHttpActionResult GetJobListing(int id)
        {
            var jobListing = bll.GetOne(id);

            if (jobListing == null)
                NotFound();

            return Ok(jobListing);
        }

        [Route("api/appliedjobs/{id}")]
        public IHttpActionResult GetAppliedJobs(int id)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole(RoleName.CanManageJobs))
                return Ok(bll.GetUserApplicationsForJob(id));

            return Unauthorized();
        }

        [Route("api/appliedjobs")]
        public IHttpActionResult GetAppliedUserJobs()
        {
            if(User.Identity.IsAuthenticated)
                return Ok(bll.GetAppliedJobs(User.Identity.GetUserId()));

            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageJobs)]
        public IHttpActionResult CreateJobListing(JobListingDto jobListingDto)
        {
            if (ModelState.IsValid)
                return BadRequest();

            var createdJobListing = bll.Create(jobListingDto);

            return Created(new Uri(Request.RequestUri + "/" + createdJobListing.Id), createdJobListing);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageJobs)]
        public IHttpActionResult UpdateJobListing(int id, JobListingDto jobListingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var success = bll.Update(id, jobListingDto);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageJobs)]
        public IHttpActionResult DeleteJobListing(int id)
        {
            var success = bll.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
