using ARMS.Application.Interfaces.Repository;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;

namespace ARMS.Infrastructure.Repositories
{
    public class PermissionRepository : Repository<ApplicationDbContext, PermissionModel>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context)
            : base(context)
        { }
    }
}
