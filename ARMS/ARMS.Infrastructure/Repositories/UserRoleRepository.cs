using ARMS.Application.Interfaces.Repository;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;

namespace ARMS.Infrastructure.Repositories
{
    public class UserRoleRepository : Repository<ApplicationDbContext, UserRoleModel>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
