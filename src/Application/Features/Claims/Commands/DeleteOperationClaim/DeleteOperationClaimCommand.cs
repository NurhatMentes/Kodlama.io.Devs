using Application.Features.Claims.Dtos;
using Application.Features.Claims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand:IRequest<DeletedOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteOperationClaimCommandHandler:IRequestHandler<DeleteOperationClaimCommand,DeletedOperationClaimDto>
        {
            IOperationClaimRepository _repository;
            IMapper _mapper;
            OperationClaimBusinessRules _rules;

            public DeleteOperationClaimCommandHandler(IOperationClaimRepository repository, IMapper mapper, OperationClaimBusinessRules rules)
            {
                _repository = repository;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _rules.ClaimShouldExistWhenRequested(request.Id);
                OperationClaim? operationClaim = await _repository.GetAsync(x => x.Id == request.Id);
                OperationClaim updatedOperationClaim = await _repository.DeleteAsync(operationClaim);
                DeletedOperationClaimDto deletedOperationClaimDto = _mapper.Map<DeletedOperationClaimDto>(updatedOperationClaim);
                return deletedOperationClaimDto;
            }
        }
    }
}
