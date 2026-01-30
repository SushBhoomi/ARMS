using ARMS.Application.Common;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities.QueryModels;

namespace ARMS.Application.Entities.Users.Services.Interfaces
{
    public interface IUserRequestService : IServiceBase<UserRequestModel, UserRequestViewModel>
    {
        Task<UserRequestViewModel> GetAsync(long id);

        Task<string> ApproveAsync(long id, UserRequestViewModel viewModel, string currentUser);

        Task<string> RejectAsync(long id, UserRequestViewModel viewModel, string currentUser);
    }
}
