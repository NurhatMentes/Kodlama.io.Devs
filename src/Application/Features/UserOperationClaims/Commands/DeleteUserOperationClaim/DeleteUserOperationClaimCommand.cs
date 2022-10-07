using Application.Features.Technologies.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand:IRequest<DeletedUserOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteUserOperationClaimCommandHandler:IRequestHandler<DeleteUserOperationClaimCommand,DeletedUserOperationClaimDto>
        {
            private readonly IMapper _mapper;
            private readonly IUserOperationClaimRepository _repository;
            private readonly UserOperationClaimBusinessRules _rules;

            public DeleteUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository repository, UserOperationClaimBusinessRules rules)
            {
                _mapper = mapper;
                _repository = repository;
                _rules = rules;
            }

            public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.UserClaimShouldExistWhenRequested(request.Id);

                UserOperationClaim? userOperationClaim = await _repository.GetAsync(x => x.Id == request.Id);
                UserOperationClaim deletedUserOperationClaim = await _repository.DeleteAsync(userOperationClaim);
                DeletedUserOperationClaimDto mappedUserOperation = _mapper.Map<DeletedUserOperationClaimDto>(deletedUserOperationClaim);
                return mappedUserOperation;
            }
        }
    }
}
