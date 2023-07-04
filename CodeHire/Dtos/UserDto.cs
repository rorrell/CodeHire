﻿using CodeHire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeHire.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Resume? Resume { get; set; }
    }
}