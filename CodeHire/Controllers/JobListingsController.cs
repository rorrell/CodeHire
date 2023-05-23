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
        private LanguagesBusinessLogic lbll;

        public JobListingsController()
        {
            bll = new JobListingsBusinessLogic();
            lbll = new LanguagesBusinessLogic();
        }

        protected override void Dispose(bool disposing)
        {
            bll.Dispose();
            lbll.Dispose();
        }

        public ViewResult Index()
        {
            if (Request.IsAuthenticated)
                return View("IndexManager");

            return View();
        }

        [Authorize(Roles = RoleName.CanManageJobs)]
        public ViewResult IndexManager()
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
                Languages = lbll.GetLanguages().ToList(),
                JobListing = new JobListingDto(),
                SelectedLanguageNames = new List<string>()
            };

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles=RoleName.CanManageJobs)]
        public ActionResult Save(JobListingFormViewModel jobListingForm)
        {
            if (!ModelState.IsValid)
            {
                return View("JobListingForm", jobListingForm);
            }

            //have to get from db because jobListingForm.Languages is null at this point
            //and the business logic layer doesn't know about JobListingFormViewModel
            jobListingForm.JobListing.Languages =
                lbll.GetLanguages().Where(
                    l => jobListingForm.SelectedLanguageNames.Contains(l.Name)).ToList();

            if (jobListingForm.JobListing.Id == 0)
                bll.CreateJobListing(jobListingForm.JobListing);
            else
            {
                if (!bll.UpdateJobListing(jobListingForm.JobListing.Id, jobListingForm.JobListing))
                    return new HttpNotFoundResult();
            }

            return RedirectToAction("Index", "JobListings");
        }

        public ActionResult Edit(int id)
        {
            var jobListing = bll.GetJobListing(id);

            if (jobListing == null)
                return HttpNotFound();

            var viewModel = new JobListingFormViewModel
            {
                JobListing = jobListing,
                Languages = lbll.GetLanguages().ToList(),
                SelectedLanguageNames = jobListing.Languages.Select(
                    l => l.Name).ToList()
            };

            return View("JobListingForm", viewModel);
        }
    }
}