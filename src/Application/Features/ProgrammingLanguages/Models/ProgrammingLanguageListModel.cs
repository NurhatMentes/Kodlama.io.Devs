using Application.Features.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models
{
    public class ProgrammingLanguageListModel
    {
        public IList<ProgrammingLanguageListDto> Items { get; set; }
    }
}
