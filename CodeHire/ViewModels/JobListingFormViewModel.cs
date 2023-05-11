using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeHire.Dtos;
using CodeHire.Models;

namespace CodeHire.ViewModels
{
    public class JobListingFormViewModel
    {
        public JobListingDto JobListing { get; set; }
        public IEnumerable<LanguageDto> Languages { get; set; }
    }
}