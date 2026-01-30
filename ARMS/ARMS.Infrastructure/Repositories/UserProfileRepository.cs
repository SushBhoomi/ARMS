using ARMS.Application.Interfaces.Repository;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ARMS.Infrastructure.Repositories
{
    public class UserProfileRepository : Repository<ApplicationDbContext, UserProfileModel>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<RoleModel>> GetRoleByUserNameAsync(string username)
        {
            var result = await this.GetQueryable()
                                .Include(u => u.UserRoles)
                                .ThenInclude(r => r.Role.RolePermissions)
                                .SingleOrDefaultAsync(r => r.Username == username);

            return result.UserRoles.Select(x => x.Role).ToList();
        }
    }
}
