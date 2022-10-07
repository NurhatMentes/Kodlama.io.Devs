using Application.Features.Claims.Dtos;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Models
{
    public class OperationClaimListModel
    {
        public List<OperationClaimListDto> Items { get; set; }
    }
}
