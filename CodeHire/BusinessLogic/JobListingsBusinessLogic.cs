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

        public JobListingsBusinessLogic()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<LanguageDto> GetLanguages()
        {
            return _context.Languages.Select(Mapper.Map<Language, LanguageDto>);
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

        public JobListingDto CreateJobListing(JobListingDto jobListingDto)
        {
            var jobListing = Mapper.Map<JobListingDto, JobListing>(jobListingDto);
            _context.JobListings.Add(jobListing);
            _context.SaveChanges();

            jobListingDto.Id = jobListing.Id;

            return jobListingDto;
        }

        public bool UpdateJobListing(int id, JobListingDto jobListingDto)
        {
            var jobListingInDb = _context.JobListings.SingleOrDefault(j => j.Id == id);

            if (jobListingInDb == null)
                return false;

            Mapper.Map(jobListingDto, jobListingInDb);

            _context.SaveChanges();

            return true;
        }
    }
}