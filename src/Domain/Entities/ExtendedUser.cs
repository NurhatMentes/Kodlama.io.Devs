using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class ExtendedUser:User
    {
        public string Github { get; set; }
    }
}
