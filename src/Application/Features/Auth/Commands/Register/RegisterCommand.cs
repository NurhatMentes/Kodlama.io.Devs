﻿using Application.Auth.Dtos;
using Application.Auth.Rules;
using Application.Features.Auth.Dtos;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand :  IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public RegisterCommandHandler( IUserRepository userRepository, IAuthService authService)
            {
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out var passWordHash, out var passwordSalt);

                ExtendedUser user = new ExtendedUser
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passWordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                };

                ExtendedUser createdUser = await _userRepository.AddAsync(user);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken,
                };

                return registeredDto;
            }
        }
    }
}