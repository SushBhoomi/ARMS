using ARMS.Application.Interfaces.Repository;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;

namespace ARMS.Infrastructure.Repositories
{
    public class RoleRepository : Repository<ApplicationDbContext, RoleModel>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context)
            : base(context)
        { }
    }
}
