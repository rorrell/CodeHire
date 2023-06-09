﻿using CodeHire.Dtos;
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
            var resume = _context.Resumes.Include(r => r.WorkHistory)
                .Include(r => r.User)
                .SingleOrDefault(r => r.User.Id == userId);
            
            return Mapper.Map<Resume, ResumeDto>(resume);
        }

        public ResumeDto GetById(int id)
        {
            var resume = _context.Resumes.SingleOrDefault(r => r.Id == id);

            return Mapper.Map<Resume, ResumeDto>(resume);
        }

        public ResumeDto Create(ResumeDto resumeDto, string userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            var resume = Mapper.Map<ResumeDto, Resume>(resumeDto);
            user.Resume = resume;

            _context.SaveChanges();

            resumeDto.Id = user.Resume.Id;

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