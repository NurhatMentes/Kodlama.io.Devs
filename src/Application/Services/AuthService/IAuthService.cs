using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<AccessToken> CreateAccessToken(ExtendedUser extendUser);
    }
}
