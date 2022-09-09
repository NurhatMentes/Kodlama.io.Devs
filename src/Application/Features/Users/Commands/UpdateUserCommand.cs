using Application.Auth.Dtos;
using Application.Auth.Rules;
using Application.Features.Auth.Commands.Login;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<UpdatedUserDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string GitHub { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IMapper mapper)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserShouldExistWhenRequested(request.Email);

                ExtendedUser user = await _userRepository.GetAsync(u => u.Email == request.Email);

                 _mapper.Map(request, user);

                await _userRepository.UpdateAsync(user);

                UpdatedUserDto updatedUserDto = _mapper.Map<UpdatedUserDto>(user);

                return updatedUserDto;
            }
        }
    }
}