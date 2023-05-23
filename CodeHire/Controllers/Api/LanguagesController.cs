using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeHire.BusinessLogic;
using CodeHire.Dtos;

namespace CodeHire.Controllers.Api
{
    public class LanguagesController : ApiController
    {
        private LanguagesBusinessLogic bll;

        public LanguagesController()
        {
            bll = new LanguagesBusinessLogic();
        }

        protected override void Dispose(bool disposing)
        {
            bll.Dispose();
        }

        public IHttpActionResult GetLanguages(string query = null)
        {
            return Ok(bll.GetLanguages(query));
        }
    }
}
