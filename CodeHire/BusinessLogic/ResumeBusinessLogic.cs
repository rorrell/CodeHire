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
        public ResumeBusinessLogic(ApplicationDbContext context) : base(context) { }

        public ResumeDto GetByUser(string userId)
        {
            var resume = _context.Resumes.Include(r => r.WorkHistory)
                .Include(r => r.User)
                .Include(r => r.Skills)
                .SingleOrDefault(r => r.User.Id == userId);

            return Mapper.Map<Resume, ResumeDto>(resume);
        }

        public ResumeDto GetById(int id)
        {
            var resume = _context.Resumes
                .Include(r => r.Skills)
                .SingleOrDefault(r => r.Id == id);

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

        public bool Update(int id, ResumeDto resumeDto, List<int> selectedSkillIds = null)
        {
            var resumeInDb = _context.Resumes
                .Include(r => r.Skills)
                .SingleOrDefault(r => r.Id == id);

            if (resumeInDb == null)
                return false;

            var resumeSkills = new List<Skill>(resumeInDb.Skills);

            Mapper.Map(resumeDto, resumeInDb);

            resumeInDb.Skills.AddRange(resumeSkills);
            if (selectedSkillIds != null)
            {
                var skills = _context.Skills.
                    Where(s => selectedSkillIds.Contains(s.Id)).ToList();
                resumeInDb.Skills.AddRange(skills);
            }

            _context.SaveChanges();

            return true;
        }

        public bool DeleteSkill(int resumeId, int skillId)
        {
            var resume = _context.Resumes
                .Include(r => r.Skills)
                .SingleOrDefault(r => r.Id == resumeId);

            if (resume == null)
                return false;

            var skill = _context.Skills
                .SingleOrDefault(s => s.Id == skillId);

            if (skill == null)
                return false;

            bool success = resume.Skills.Remove(skill);

            if (success)
            {
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}