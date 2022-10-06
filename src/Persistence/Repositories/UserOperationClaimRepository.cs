using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Context;
using Application.Services.Repositories;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(BaseDbContext context) : base(context)
        {
        }
    }
}