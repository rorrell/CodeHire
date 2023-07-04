using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CodeHire.Dtos;
using CodeHire.Models;

namespace CodeHire.BusinessLogic
{
    public class LanguagesBusinessLogic : BasicBusinessLogicImplementor<LanguageDto>
    {
        public LanguagesBusinessLogic(ApplicationDbContext context) : base(context) {}

        public IEnumerable<LanguageDto> GetAll(string query = null)
        {
            var languagesQuery = _context.Languages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                languagesQuery = languagesQuery
                    .Where(l => l.Name.Contains(query));

            return languagesQuery
                .ToList()
                .Select(Mapper.Map<Language, LanguageDto>);
        }

    }
}