using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Dtos
{
    public class TokenDto
    {
        public AccessToken AccessToken { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
