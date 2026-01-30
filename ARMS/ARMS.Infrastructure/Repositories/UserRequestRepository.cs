using ARMS.Application.Interfaces.Repository;
using ARMS.Core.Entities.QueryModels;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;

namespace ARMS.Infrastructure.Repositories
{
    public class UserRequestRepository : Repository<ApplicationDbContext, UserRequestModel>, IUserRequestRepository
    {
        public UserRequestRepository(ApplicationDbContext context)
            : base(context)
        { }

        protected override IQueryable<UserRequestModel> GetQueryable()
        {
            IQueryable<UserRequestModel> query = Context.Set<UserRequestModel>();
            return query;
        }
    }
}
