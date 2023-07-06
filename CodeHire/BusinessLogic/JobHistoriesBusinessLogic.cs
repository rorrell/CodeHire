using AutoMapper;
using CodeHire.Dtos;
using CodeHire.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeHire.BusinessLogic
{
    public class JobHistoriesBusinessLogic : FullBusinessLogicImplementor<JobHistoryDto>
    {
        private ResumeBusinessLogic rbll;
        public JobHistoriesBusinessLogic(ApplicationDbContext context) : base(context) 
        {
            rbll = new ResumeBusinessLogic(context);
        }

        public override IEnumerable<JobHistoryDto> GetAll()
        {
            return _context.JobHistories
                .ToList()
                .Select(Mapper.Map<JobHistory, JobHistoryDto>);
        }

        public override JobHistoryDto GetOne(int id)
        {
            var jobHistory = _context.JobHistories
                .SingleOrDefault(j => j.Id == id);

            if (jobHistory == null)
                return null;

            return Mapper.Map<JobHistory, JobHistoryDto>(jobHistory);
        }

        public JobHistoryDto Create(JobHistoryDto jobHistoryDto, string userId)
        {
            var user = _context.Users.Include(u => u.Resume).SingleOrDefault(u => u.Id == userId);
            if(user != null)
            {
                var resume = user.Resume;
                var jobHistory = Mapper.Map<JobHistoryDto, JobHistory>(jobHistoryDto);
                resume.WorkHistory.Add(jobHistory);

                _context.SaveChanges();

                jobHistoryDto.Id = jobHistory.Id;

                return jobHistoryDto;
            }

            return null;
        }

        public bool Update(int id, JobHistoryDto jobHistoryDto)
        {
            var jobHistoryInDb = _context.JobHistories.SingleOrDefault(j => j.Id == id);

            if (jobHistoryInDb == null)
                return false;

            Mapper.Map(jobHistoryDto, jobHistoryInDb);

            _context.SaveChanges();

            return true;
        }

        public override bool Delete(int id)
        {
            var jobHistoryInDb = _context.JobHistories.SingleOrDefault(j => j.Id == id);

            if (jobHistoryInDb == null)
                return false;

            _context.JobHistories.Remove(jobHistoryInDb);
            _context.SaveChanges();

            return true;
        }
    }
}