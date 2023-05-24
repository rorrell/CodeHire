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

namespace CodeHire.Controllers.Api
{
    public class JobListingsController : ApiController
    {
        private JobListingsBusinessLogic bll;

        public JobListingsController()
        {
            bll = new JobListingsBusinessLogic();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                bll.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetJobListings()
        {
            return Ok(bll.GetJobListings());
        }

        public IHttpActionResult GetJobListing(int id)
        {
            var jobListing = bll.GetJobListing(id);

            if (jobListing == null)
                NotFound();

            return Ok(jobListing);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageJobs)]
        public IHttpActionResult CreateJobListing(JobListingDto jobListingDto)
        {
            if (ModelState.IsValid)
                return BadRequest();

            var createdJobListing = bll.CreateJobListing(jobListingDto);

            return Created(new Uri(Request.RequestUri + "/" + createdJobListing.Id), createdJobListing);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageJobs)]
        public IHttpActionResult UpdateJobListing(int id, JobListingDto jobListingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var success = bll.UpdateJobListing(id, jobListingDto);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageJobs)]
        public IHttpActionResult DeleteJobListing(int id)
        {
            var success = bll.DeleteJobListing(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
