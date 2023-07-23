using AutoMapper;
using CodeHire.Dtos;
using CodeHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeHire.BusinessLogic
{
    public class SkillsBusinessLogic : BasicBusinessLogicImplementor<SkillDto>
    {
        public SkillsBusinessLogic(ApplicationDbContext context) : base(context) { }

        public IEnumerable<SkillDto> GetAll(string query = null)
        {
            var skillQuery = _context.Skills.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                skillQuery = skillQuery
                    .Where(s => s.Name.Contains(query));

            return skillQuery
                .ToList()
                .Select(Mapper.Map<Skill, SkillDto>);
        }

        public SkillDto Create(SkillDto skillDto)
        {
            var skill = Mapper.Map<SkillDto, Skill>(skillDto);

            _context.Skills.Add(skill);
            _context.SaveChanges();

            skillDto.Id = skill.Id;
            return skillDto;
        }
    }
}