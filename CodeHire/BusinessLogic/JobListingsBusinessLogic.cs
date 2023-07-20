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
    public class JobListingsBusinessLogic : FullBusinessLogicImplementor<JobListingDto>
    {
        public JobListingsBusinessLogic(ApplicationDbContext context) : base(context) {}

        public override IEnumerable<JobListingDto> GetAll()
        {
            return _context.JobListings
                .Include(j => j.Languages)
                .ToList()
                .Select(Mapper.Map<JobListing, JobListingDto>);
        }

        public override JobListingDto GetOne(int id)
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
        public JobListingDto Create(JobListingDto jobListingDto, List<byte> selectedLanguageIds = null)
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
        public bool Update(int id, JobListingDto jobListingDto, List<byte> selectedLanguageIds = null)
        {
            var jobListingInDb = _context.JobListings
                .Include(j => j.Languages)
                .Include(j => j.JobListingApplicationUsers)
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

        public override bool Delete(int id)
        {
            var jobListingInDb = _context.JobListings.SingleOrDefault(j => j.Id == id);

            if (jobListingInDb == null)
                return false;

            _context.JobListings.Remove(jobListingInDb);
            _context.SaveChanges();

            return true;
        }

        public bool ApplyOrSaveJob(int jobId, string userId, JobListingType type = JobListingType.Applied)
        {
            var jobListingInDb = _context.JobListings.
                Include(j => j.Languages).
                Include(j => j.JobListingApplicationUsers).
                SingleOrDefault(j => j.Id == jobId);

            if (jobListingInDb == null)
                return false;

            var userInDb = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (userInDb == null)
                return false;

            jobListingInDb.JobListingApplicationUsers.Add(new JobListingApplicationUser
            {
                ApplicationUser = userInDb,
                JobListing = jobListingInDb,
                Type = type
            });

            _context.SaveChanges();

            return true;
        }

        public bool ApplyToSaved(int jobId, string userId)
        {
            var jobListingApplicationUserInDb = _context.JobListingApplicationUsers
                .SingleOrDefault(j => j.JobListing_Id == jobId && j.ApplicationUser_Id == userId);

            if (jobListingApplicationUserInDb == null)
                return false;

            jobListingApplicationUserInDb.Type = JobListingType.Applied;

            _context.SaveChanges();

            return true;
        }

        public IEnumerable<JobListingDto> GetAppliedOrSavedJobs(string userId, JobListingType type = JobListingType.Applied)
        {
            var userInDb = _context.Users
                .Include(u => u.JobListingApplicationUsers.Select(j => j.JobListing.Languages))
                .SingleOrDefault(u => u.Id == userId);

            if (userInDb == null)
                return null;

            return userInDb.JobListingApplicationUsers
                .Where(j => j.Type == type)
                .Select(j => j.JobListing)
                .ToList()
                .Select(Mapper.Map<JobListing, JobListingDto>);
        }

        public IEnumerable<UserDto> GetUserApplicationsForJob(int jobId)
        {
            var jobListing = _context.JobListings
                .Include(j => j.JobListingApplicationUsers.Select(j => j.ApplicationUser))
                .First(j => j.Id == jobId);

            return jobListing.JobListingApplicationUsers
                .Select(j => Mapper.Map<ApplicationUser, UserDto>(j.ApplicationUser));
        }

        public bool DeleteSavedJob(int jobListingId, string userId)
        {
            var jobListingApplicationUserInDb = _context.JobListingApplicationUsers
                .SingleOrDefault(j => j.JobListing_Id == jobListingId && j.ApplicationUser_Id == userId);

            if (jobListingApplicationUserInDb == null)
                return false;

            _context.JobListingApplicationUsers.Remove(jobListingApplicationUserInDb);
            _context.SaveChanges();

            return true;

        }
    }
}