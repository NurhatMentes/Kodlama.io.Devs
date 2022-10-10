using Application.Features.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Model
{
    public class UserOperationClaimListModel
    {
        public IList<UserOperationClaimListDto> Items { get; set; }
    }
}
