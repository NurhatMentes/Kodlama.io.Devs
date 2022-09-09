using Application.Auth.Dtos;
using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Commands;
using Application.Features.Users.Dtos;
using Domain.Entities;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExtendedUser, UpdateUserCommand>().ReverseMap();
            CreateMap<ExtendedUser, UpdatedUserDto>().ReverseMap();
        }
    }
}