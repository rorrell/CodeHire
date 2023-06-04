using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeHire.BusinessLogic;
using CodeHire.Dtos;
using CodeHire.Models;

namespace CodeHire.Controllers.Api
{
    public class LanguagesController : ApiController
    {
        private LanguagesBusinessLogic bll;

        public LanguagesController()
        {
            bll = new LanguagesBusinessLogic(new ApplicationDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                bll.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetLanguages(string query = null)
        {
            return Ok(bll.GetAll(query));
        }
    }
}
