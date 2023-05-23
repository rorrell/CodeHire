using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CodeHire.Dtos;
using CodeHire.Models;

namespace CodeHire.BusinessLogic
{
    public class LanguagesBusinessLogic : IDisposable
    {
        private ApplicationDbContext _context;

        public LanguagesBusinessLogic()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<LanguageDto> GetLanguages(string query = null)
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