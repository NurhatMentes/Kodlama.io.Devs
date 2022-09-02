using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguagesCommandValidator : AbstractValidator<CreateProgrammingLanguagesCommand>
    {
        public CreateProgrammingLanguagesCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
