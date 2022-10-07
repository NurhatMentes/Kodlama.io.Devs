using Application.Features.UserOperationClaims.Constants;
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

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _repository = userOperationClaimRepository;
        }

        public async Task ClaimUserCanNotBeDuplicatedWhenInserted(int id)
        {
            IPaginate<UserOperationClaim> result = await _repository.GetListAsync(p => p.User.Id == id);
            if (result.Items.Any()) throw new BusinessException(Messages.UserCanNotBeDuplicatedWhenInserted);
        }

        public async Task ClaimNameCanNotBeDuplicatedWhenUpdated(int id)
        {
            IPaginate<UserOperationClaim> result = await _repository.GetListAsync(p => p.User.Id == id);
            if (result.Items.Any()) throw new BusinessException(Messages.UserCanNotBeDuplicatedWhenInserted);
        }

        public async Task UserClaimShouldExistWhenRequested(int id)
        {
            UserOperationClaim? operationClaim = await _repository.GetAsync(p => p.User.Id == id);
            if (operationClaim == null) throw new BusinessException(Messages.UserShouldExistWhenRequested);
        }

        public async Task ClaimShouldExistWhenRequested(int id)
        {
            UserOperationClaim? operationClaim = await _repository.GetAsync(p => p.OperationClaim.Id == id);
            if (operationClaim == null) throw new BusinessException(Messages.ClaimShouldExistWhenRequested);
        }
    }
}
