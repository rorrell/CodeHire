using CodeHire.Dtos;
using CodeHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AutoMapper;

namespace CodeHire.BusinessLogic
{
    public class ResumeBusinessLogic : BasicBusinessLogicImplementor<ResumeDto>
    {
        public ResumeBusinessLogic(ApplicationDbContext context) : base(context) {}

        public ResumeDto GetByUser(string userId)
        {
            var resume = _context.Users.Single(u => u.Id == userId).Resume;

            return Mapper.Map<Resume, ResumeDto>(resume);
        }

        public ResumeDto Create(ResumeDto resumeDto)
        {
            var resume = Mapper.Map<ResumeDto, Resume>(resumeDto);

            _context.Resumes.Add(resume);
            _context.SaveChanges();

            resumeDto.Id = resume.Id;

            return resumeDto;
        }
        public bool Update(int id, ResumeDto resumeDto)
        {
            var resumeInDb = _context.Resumes
                .SingleOrDefault(r => r.Id == id);

            if (resumeInDb == null)
                return false;

            Mapper.Map(resumeDto, resumeInDb);

            _context.SaveChanges();

            return true;
        }

    }
}