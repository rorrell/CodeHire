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
    public class ResumeController : Controller
    {
        private ResumeBusinessLogic bll;

        public ResumeController()
        {
            var context = new ApplicationDbContext();
            bll = new ResumeBusinessLogic(context);
        }

        public ViewResult Details(string id)
        {
            var resume = bll.GetByUser(id);

            return View(resume);
        }

        public ActionResult ResumeForm()
        {
            var resume = bll.GetByUser(User.Identity.GetUserId());

            return View(resume);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bll.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(ResumeDto resume)
        {
            if (string.IsNullOrEmpty(resume.Summary))
                return View("ResumeForm", resume);

            if (resume.Id == 0)
            {
                bll.Create(resume, User.Identity.GetUserId());
            }
            else
            {
                if (!bll.Update(resume.Id, resume))
                    return new HttpNotFoundResult();
            }

            return RedirectToAction("Index", "JobListings");
        }
    }
}