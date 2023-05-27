using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CodeHire.Dtos;
using CodeHire.Models;
using System.Data.Entity;

namespace CodeHire.BusinessLogic
{
    public class JobListingsBusinessLogic : IDisposable
    {
        private ApplicationDbContext _context;

        public JobListingsBusinessLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<JobListingDto> GetJobListings()
        {
            return _context.JobListings
                .Include(j => j.Languages)
                .ToList()
                .Select(Mapper.Map<JobListing, JobListingDto>);
        }

        public JobListingDto GetJobListing(int id)
        {
            var jobListing = _context.JobListings
                .Include(j => j.Languages)
                .SingleOrDefault(j => j.Id == id);

            if (jobListing == null)
                return null;

            return Mapper.Map<JobListing, JobListingDto>(jobListing);
        }

        //The selectedLanguageIds is to support the case where we are coming from the form and so the
        //languages are there rather than attached to the job listing Dto
        public JobListingDto CreateJobListing(JobListingDto jobListingDto, List<byte> selectedLanguageIds = null)
        {
            var jobListing = Mapper.Map<JobListingDto, JobListing>(jobListingDto);

            if (selectedLanguageIds != null)
            {
                jobListing.Languages.Clear();
                var langs = _context.Languages.
                    Where(l => selectedLanguageIds.Contains(l.Id)).ToList();
                jobListing.Languages.AddRange(langs);
            }

            _context.JobListings.Add(jobListing);
            _context.SaveChanges();

            jobListingDto.Id = jobListing.Id;

            return jobListingDto;
        }

        //The selectedLanguageIds is to support the case where we are coming from the form and so the
        //languages are there rather than attached to the job listing Dto
        public bool UpdateJobListing(int id, JobListingDto jobListingDto, List<byte> selectedLanguageIds = null)
        {
            var jobListingInDb = _context.JobListings
                .Include(j => j.Languages)
                .SingleOrDefault(j => j.Id == id);

            if (jobListingInDb == null)
                return false;

            Mapper.Map(jobListingDto, jobListingInDb);

            if (selectedLanguageIds != null)
            {
                jobListingInDb.Languages.Clear();
                var langs = _context.Languages.
                    Where(l => selectedLanguageIds.Contains(l.Id)).ToList();
                jobListingInDb.Languages.AddRange(langs);
            }

            _context.SaveChanges();

            return true;
        }

        public bool DeleteJobListing(int id)
        {
            var jobListingInDb = _context.JobListings.SingleOrDefault(j => j.Id == id);

            if (jobListingInDb == null)
                return false;

            _context.JobListings.Remove(jobListingInDb);
            _context.SaveChanges();

            return true;
        }
    }
}