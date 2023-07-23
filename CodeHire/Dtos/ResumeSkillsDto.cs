using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeHire.Dtos
{
    public class ResumeSkillsDto
    {
        public int ResumeId { get; set; }

        public List<int> SkillIds { get; set; }
    }
}