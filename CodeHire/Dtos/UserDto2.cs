using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeHire.Dtos
{
    public class UserDto2
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ResumeWithoutUserDto? Resume { get; set; }
    }
}