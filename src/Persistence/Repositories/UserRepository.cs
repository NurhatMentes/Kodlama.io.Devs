using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<ExtendedUser, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context)
        {
        }
    }
}