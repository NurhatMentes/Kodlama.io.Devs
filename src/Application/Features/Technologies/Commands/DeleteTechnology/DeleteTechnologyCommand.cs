using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.DeleteProgrammingLanguage;
using Application.Features.Dtos;
using Application.Features.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand:IRequest<deletedTechnologyDto>
    {
        public int Id { get; set; }

        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, deletedTechnologyDto>
        {
            private readonly ITechnologyRepository _repository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _businessRules;

            public DeleteTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<deletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.TechnologyShouldExistWhenRequested(request.Id);
                Technology? technology = await _repository.GetAsync(x => x.Id == request.Id);
                Technology deletedTechnology = await _repository.DeleteAsync(technology);
                deletedTechnologyDto deletedTechnologyDto = _mapper.Map<deletedTechnologyDto>(deletedTechnology);
                return deletedTechnologyDto;
            }
        }
    }
}
