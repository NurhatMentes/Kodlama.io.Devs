using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _repository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _repository = technologyRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await _repository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.TechnologyNameCanNotBeDuplicatedWhenInserted);
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(string name)
        {
            IPaginate<Technology> result = await _repository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException(Messages.TechnologyNameCanNotBeDuplicatedWhenInserted);
        }
        public async Task TechnologyShouldExistWhenRequested(int id)
        {
            Technology? technology = await _repository.GetAsync(p => p.Id == id);
            if (technology == null) throw new BusinessException(Messages.TechnologyShouldExistWhenRequested);
        }
    }
}
