﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.CreateProgrammingLanguage;
using Application.Features.Commands.DeleteProgrammingLanguage;
using Application.Features.Commands.UpdateProgrammingLanguage;
using Application.Features.Dtos;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {

            CreateMap<Technology, CreateTechnologyCommand>().ForMember(c => c.ProgrammingLanguageId, opt => opt.MapFrom(c => c.ProgrammingLanguageId)).ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeleteTechnologyCommand>().ReverseMap();
            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
            CreateMap<Technology, UpdateTechnologyCommand>().ForMember(c => c.ProgrammingLanguageId, opt => opt.MapFrom(c => c.ProgrammingLanguageId)).ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, TechnologyListDto>()
                .ForMember(c => c.ProgrammingName, opt => opt.MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel >().ReverseMap();
        }
    }
}
