﻿using Application.Features.Dtos;
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

namespace Application.Features.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguagesCommand : IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }
        public class CreateProgrammingLanguagesCommandHandler : IRequestHandler<CreateProgrammingLanguagesCommand, CreatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _repository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _businessRules;

            public CreateProgrammingLanguagesCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguagesCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);

                ProgrammingLanguage createdProgrammingLanguage = await _repository.AddAsync(mappedProgrammingLanguage);

                CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);

                return createdProgrammingLanguageDto;
            }
        }
    }
}