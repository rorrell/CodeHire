using CodeHire.BusinessLogic;
using CodeHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeHire.Controllers.Api
{
    public class JobHistoriesController : ApiController
    {
        private JobHistoriesBusinessLogic bll;

        public JobHistoriesController()
        {
            bll = new JobHistoriesBusinessLogic(new ApplicationDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                bll.Dispose();
            base.Dispose(disposing);
        }

        public IHttpActionResult GetJobHistories()
        {
            return Ok(bll.GetAll());
        }

        [HttpDelete]
        public IHttpActionResult DeleteJobHistory(int id)
        {
            var success = bll.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
