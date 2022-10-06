
using Application.Auth.Dtos;
using Application.Auth.Rules;
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

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<RefreshedTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, RefreshedTokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, IMapper mapper, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
            }
            public async Task<RefreshedTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                ExtendedUser? user = await _userRepository.GetAsync(u => u.Email == request.Email);

                _authBusinessRules.UserShouldExistWhenRequested(user!);

                var result = HashingHelper.VerifyPasswordHash(request.Password, user!.PasswordHash, user.PasswordSalt);
                var roles = await _operationClaimRepository.GetListAsync(o => o.Id==user.Id);

                AccessToken accessToken = _tokenHelper.CreateToken(user, roles.Items);

                RefreshedTokenDto tokenDto = _mapper.Map<RefreshedTokenDto>(accessToken);

                return tokenDto;
            }
        }
    }
}