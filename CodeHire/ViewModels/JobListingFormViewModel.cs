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
        public List<string> SelectedLanguageNames { get; set; }
        public List<LanguageDto> Languages { get; set; } = new();
    }
}