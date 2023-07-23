using CodeHire.BusinessLogic;
using CodeHire.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeHire.Controllers
{
    public class SkillsController : Controller
    {
        SkillsBusinessLogic bll;
        private ResumeBusinessLogic rbll;

        public SkillsController()
        {
            var context = new ApplicationDbContext();
            bll = new SkillsBusinessLogic(context);
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

        [Authorize]
        public ActionResult Index()
        {
            var resume = rbll.GetByUser(User.Identity.GetUserId());
            return View(resume);
        }
    }
}