using ARMS.Core.Entities;

namespace ARMS.Application.Interfaces.Repository
{
    public interface IUserProfileRepository : IRepository<UserProfileModel>
    {
        Task<IEnumerable<RoleModel>> GetRoleByUserNameAsync(string username);
    }
}
