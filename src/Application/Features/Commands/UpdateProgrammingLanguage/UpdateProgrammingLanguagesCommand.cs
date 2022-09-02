using Application.Features.Dtos;
using Application.Features.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguagesCommand:IRequest<UpdatedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateProgrammingLanguagesCommandHandler : IRequestHandler<UpdateProgrammingLanguagesCommand, UpdatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _repository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _businessRules;

            public UpdateProgrammingLanguagesCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguagesCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgrammingLanguageShouldExistWhenRequested(request.Id);
                await _businessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(request.Name);

                ProgrammingLanguage? mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage? uptadetProgrammingLanguage = await _repository.UpdateAsync(mappedProgrammingLanguage);
                UpdatedProgrammingLanguageDto mappedUpdatedProgrammingLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(uptadetProgrammingLanguage);
                return mappedUpdatedProgrammingLanguageDto;
            }
        }
    }
}