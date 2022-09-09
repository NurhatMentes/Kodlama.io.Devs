using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries
{
    public class GetListTechnologyQuery:IRequest<TechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
        {
            private readonly ITechnologyRepository _modelRepository;
            private readonly IMapper _mapper;

            public GetListTechnologyQueryHandler(ITechnologyRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
            {
                //car model
                IPaginate<Technology> technologiesAsync = await _modelRepository.GetListAsync(
                    include: m => m.Include(m=>m.ProgrammingLanguage),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                //data model
                TechnologyListModel mappedModelListModel = _mapper.Map<TechnologyListModel>(technologiesAsync);

                return mappedModelListModel;
            }
        }
    }
}
