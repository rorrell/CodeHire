﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CodeHire.Dtos;
using CodeHire.Models;

namespace CodeHire.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			Mapper.CreateMap<JobListing, JobListingDto>();
			Mapper.CreateMap<JobListingDto, JobListing>();
			Mapper.CreateMap<Language, LanguageDto>();
			Mapper.CreateMap<LanguageDto, Language>();
			Mapper.CreateMap<ApplicationUser, UserDto>();
			Mapper.CreateMap<UserDto, ApplicationUser>();
			Mapper.CreateMap<ResumeDto, Resume>();
			Mapper.CreateMap<Resume, ResumeDto>();
			Mapper.CreateMap<JobHistoryDto, JobHistory>();
			Mapper.CreateMap<JobHistory, JobHistoryDto>();
		}
	}
}