using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand:IRequest<CreatedUserOperationClaimDto>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }

    public class CreateUserOperationClaimCommandHandler:IRequestHandler<CreateUserOperationClaimCommand,CreatedUserOperationClaimDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimRepository _repository;
        private readonly UserOperationClaimBusinessRules _rules;

        public CreateUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository repository, UserOperationClaimBusinessRules rules)
        {
            _mapper = mapper;
            _repository = repository;
            _rules = rules;
        }

        public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _rules.ClaimShouldExistWhenRequested(request.OperationClaimId);
            await _rules.ClaimUserCanNotBeDuplicatedWhenInserted(request.UserId);
            await _rules.UserShouldExistWhenRequested(request.UserId);



            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
            UserOperationClaim createdUserOperationClaim = await _repository.AddAsync(mappedUserOperationClaim);
            CreatedUserOperationClaimDto createdUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);
            return createdUserOperationClaimDto;
        }
    }
}
