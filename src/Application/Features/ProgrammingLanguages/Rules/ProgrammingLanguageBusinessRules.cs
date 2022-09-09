using Application.Features.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _repository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository repository)
        {
            _repository = repository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _repository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted);
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _repository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted);
        }
        public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
        {
            ProgrammingLanguage? language = await _repository.GetAsync(p => p.Id == id);
            if (language == null) throw new BusinessException(Messages.ProgrammingLanguageShouldExistWhenRequested);
        }
    }
}
