using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.UserOperationClaims.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _repository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IUserRepository _userRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository, IUserRepository userRepository)
        {
            _repository = userOperationClaimRepository;
            _operationClaimRepository = operationClaimRepository;
            _userRepository = userRepository;
        }

        //CreatedCommand
        public async Task ClaimUserCanNotBeDuplicatedWhenInserted(int id)
        {
            IPaginate<UserOperationClaim> result = await _repository.GetListAsync(p => p.User.Id == id);
            if (result.Items.Any()) throw new BusinessException(Messages.UserCanNotBeDuplicatedWhenInserted);
        }

        public async Task ClaimIdCanNotBeDuplicatedWhenUpdated(int id)
        {
            IPaginate<UserOperationClaim> result = await _repository.GetListAsync(p => p.User.Id == id);
            if (result.Items.Any()) throw new BusinessException(Messages.UserCanNotBeDuplicatedWhenInserted);
        }

        //DeletedCommand
        public async Task UserClaimShouldExistWhenRequested(int userId)
        {
            UserOperationClaim? operationClaim = await _repository.GetAsync(x=>x.UserId == userId);
            if (operationClaim == null)  throw new BusinessException(Messages.UserShouldExistWhenRequested); 
        }

        //CreatedCommand
        public async Task UserShouldExistWhenRequested(int userId)
        {
            User? user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null) throw new BusinessException(Messages.UserShouldExistWhenRequested);
        }

        //CreatedCommand
        public async Task ClaimShouldExistWhenRequested(int id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == id);
            if (operationClaim == null) throw new BusinessException(Messages.ClaimShouldExistWhenRequested);
        }

        public async Task UserOperationClaimShouldExistWhenRequested(int id)
        {
            UserOperationClaim? operationClaim = await _repository.GetAsync(p => p.Id == id);
            if (operationClaim == null) throw new BusinessException(Messages.ClaimShouldExistWhenRequested);
        }
    }
}
