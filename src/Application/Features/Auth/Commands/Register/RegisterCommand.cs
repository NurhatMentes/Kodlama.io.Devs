using Application.Auth.Dtos;
using Application.Auth.Rules;
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
    public class RegisterCommand : IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService)
            {
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<TokenDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                HashingHelper.CreatePasswordHash(request.Password, out var passWordHash, out var passwordSalt);

                ExtendedUser user = new ExtendedUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = passWordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                };

                ExtendedUser createdUser = await _userRepository.AddAsync(user);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

                TokenDto registeredDto = new TokenDto
                {
                    AccessToken = createdAccessToken
                };

                return registeredDto;
            }
        }
    }
}