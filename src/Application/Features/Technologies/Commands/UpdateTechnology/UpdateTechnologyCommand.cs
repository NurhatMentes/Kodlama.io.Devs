using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Commands.UpdateProgrammingLanguage;
using Application.Features.Dtos;
using Application.Features.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand:IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _repository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _businessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(request.Name);

                Technology mappedTechnology = _mapper.Map<Technology>(request);
                Technology updatedTechnology = await _repository.UpdateAsync(mappedTechnology);
                UpdatedTechnologyDto mappedUpdatedProgrammingLanguageDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);
                return mappedUpdatedProgrammingLanguageDto;
            }
        }
    }
}
