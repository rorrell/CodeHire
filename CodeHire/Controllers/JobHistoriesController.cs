using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using CodeHire.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeHire.Controllers
{
    public class JobHistoriesController : Controller
    {
        private JobHistoriesBusinessLogic bll;
        private ResumeBusinessLogic rbll;

        public JobHistoriesController()
        {
            var context = new ApplicationDbContext();
            bll = new JobHistoriesBusinessLogic(context);
            rbll = new ResumeBusinessLogic(context);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bll.Dispose();
                rbll.Dispose();
            }
            base.Dispose(disposing);
        }

        public ViewResult JobHistoryForm()
        {
            return View(new JobHistoryDto());
        }

        public ActionResult Edit(int id)
        {
            var jobHistory = bll.GetOne(id);

            if (jobHistory == null)
                return HttpNotFound();

            return View("JobHistoryForm", jobHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(JobHistoryDto jobHistoryDto)
        {
            if (!ModelState.IsValid)
                return View("JobHistoryForm", jobHistoryDto);

            if (jobHistoryDto.Id == 0)
                bll.Create(jobHistoryDto, User.Identity.GetUserId());
            else
            {
                if (!bll.Update(jobHistoryDto.Id, jobHistoryDto))
                    return new HttpNotFoundResult();
            }

            var resume = rbll.GetByUser(User.Identity.GetUserId());

            return RedirectToAction("ResumeForm", "Resume", resume);
        }

        [Authorize]
        public RedirectToRouteResult Cancel()
        {
            var resume = rbll.GetByUser(User.Identity.GetUserId());

            return RedirectToAction("ResumeForm", "Resume", resume);
        }
    }
}