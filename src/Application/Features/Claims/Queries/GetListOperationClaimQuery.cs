using Application.Features.Claims.Models;
using Application.Features.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Queries
{
    public class GetListOperationClaimQuery:IRequest<OperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListOperationClaimQueryHandler:IRequestHandler<GetListOperationClaimQuery,OperationClaimListModel>
        {
            IOperationClaimRepository _repository;
            private readonly IMapper _mapper;

            public GetListOperationClaimQueryHandler(IOperationClaimRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _repository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                OperationClaimListModel mappedOperationClaimListModel = _mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedOperationClaimListModel;
            }
        }
    }
}
