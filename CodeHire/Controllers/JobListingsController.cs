using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using CodeHire.Models;
using CodeHire.ViewModels;
using Microsoft.AspNet.Identity;

namespace CodeHire.Controllers
{
    public class JobListingsController : Controller
    {
        private JobListingsBusinessLogic bll;
        private LanguagesBusinessLogic lbll;

        public JobListingsController()
        {
            var context = new ApplicationDbContext();
            bll = new JobListingsBusinessLogic(context);
            lbll = new LanguagesBusinessLogic(context);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bll.Dispose();
                lbll.Dispose();
            }
            base.Dispose(disposing);
        }

        public ViewResult Index()
        {
            if (User.Identity.IsAuthenticated &&
                User.IsInRole(RoleName.CanManageJobs))
            {
                return View("IndexManager");
            }
            else if (User.Identity.IsAuthenticated)
                return View("IndexJobSeeker");

            return View();
        }

        [Authorize(Roles = RoleName.CanManageJobs)]
        public ViewResult IndexManager()
        {
            return View();
        }

        [Authorize]
        public ViewResult IndexJobSeeker()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var jobListing = bll.GetOne(id);

            if (jobListing == null)
                return HttpNotFound();

            if (User.Identity.IsAuthenticated && !User.IsInRole(RoleName.CanManageJobs))
                return View("DetailsJobSeeker", jobListing);

            return View(jobListing);
        }

        [Authorize(Roles = RoleName.CanManageJobs)]
        public ViewResult JobListingForm()
        {
            var viewModel = new JobListingFormViewModel
            {
                Languages = lbll.GetAll(null).ToList(),
                JobListing = new JobListingDto(),
                SelectedLanguageIds = new List<byte>()
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

            if (jobListingForm.JobListing.Id == 0)
                bll.Create(jobListingForm.JobListing, jobListingForm.SelectedLanguageIds);
            else
            {
                if (!bll.Update(jobListingForm.JobListing.Id, jobListingForm.JobListing, jobListingForm.SelectedLanguageIds))
                    return new HttpNotFoundResult();
            }

            return RedirectToAction("Index", "JobListings");
        }

        public ActionResult Edit(int id)
        {
            var jobListing = bll.GetOne(id);

            if (jobListing == null)
                return HttpNotFound();

            var viewModel = new JobListingFormViewModel
            {
                JobListing = jobListing,
                Languages = lbll.GetAll(null).ToList(),
                SelectedLanguageIds = jobListing.Languages.Select(
                    l => l.Id).ToList()
            };

            return View("JobListingForm", viewModel);
        }

        [Authorize]
        public ActionResult Apply(int id)
        {
            var result = bll.ApplyForJob(id, User.Identity.GetUserId());

            if (!result)
                return HttpNotFound();

            return RedirectToAction("AppliedUserJobs");
        }

        [Authorize]
        public ViewResult AppliedUserJobs()
        {
            return View();
        }

        [Authorize(Roles = RoleName.CanManageJobs)]
        public ActionResult AppliedJobs(int id)
        {
            var job = bll.GetOne(id);
            return View(job);
        }
    }
}