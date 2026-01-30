using ARMS.Application.Common;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities;

namespace ARMS.Application.Entities.Users.Services.Interfaces
{
    public interface IRoleService : IServiceBase<RoleModel, RoleViewModel>
    {
        Task<RoleViewModel> GetAsync(long id);
        Task<RoleViewModel> UpdateAsync(RoleViewModel viewModel, string userName);
    }
}
