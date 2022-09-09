using Application.Auth.Dtos;
using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<AccessToken, TokenDto>().ReverseMap();
        }
    }
}