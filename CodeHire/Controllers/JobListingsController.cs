using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using CodeHire.Models;
using CodeHire.ViewModels;

namespace CodeHire.Controllers
{
    public class JobListingsController : Controller
    {
        private JobListingsBusinessLogic bll;

        public JobListingsController()
        {
            bll = new JobListingsBusinessLogic();
        }

        protected override void Dispose(bool disposing)
        {
            bll.Dispose();
        }

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var jobListing = bll.GetJobListing(id);

            if (jobListing == null)
                return HttpNotFound();

            return View(jobListing);
        }

        [Authorize(Roles = RoleName.CanManageJobs)]
        public ViewResult JobListingForm()
        {
            var viewModel = new JobListingFormViewModel
            {
                Languages = bll.GetLanguages()
            };

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles=RoleName.CanManageJobs)]
        public ActionResult Save(JobListingDto jobListingDto)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new JobListingFormViewModel
                {
                    JobListing = jobListingDto,
                    Languages = bll.GetLanguages()
                };

                return View("JobListingForm", viewModel);
            }

            if (jobListingDto.Id == 0)
                bll.CreateJobListing(jobListingDto);
            else
            {
                if (!bll.UpdateJobListing(jobListingDto.Id, jobListingDto))
                    return new HttpNotFoundResult();
            }

            return RedirectToAction("Index", "JobListings");
        }
    }
}