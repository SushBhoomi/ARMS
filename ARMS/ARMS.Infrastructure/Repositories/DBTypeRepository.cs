using ARMS.Application.Interfaces.Repository;
using ARMS.Core;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ARMS.Infrastructure.Repositories
{
    public class DBTypeRepository : Repository<ApplicationDbContext, DBTypeMasterModel>, IDBTypeRepository
    {

        private readonly DbSet<DBTypeMasterModel> _dbSet;
        public DBTypeRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = context.Set<DBTypeMasterModel>();
        }


        public IQueryable<DBTypeMasterModel> AsQueryable(string name, string status)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => EF.Property<string>(x, "DBTypeName").StartsWith(name));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => EF.Property<string>(x, "IsActive").Equals(status));
            }

            return query;
        }

    }
}
