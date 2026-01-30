using ARMS.Application.Common;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities;
using ARMS.Core.Models;

namespace ARMS.Application.Entities.Users.Services.Interfaces
{
    public interface IUserProfileService : IServiceBase<UserProfileModel, UserProfileViewModel>
    {
        /// <summary>
        /// To get user profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserProfileViewModel> GetAsync(long id);

        /// <summary>
        /// To get user profile by user name (email address)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserProfileViewModel> GetAsync(string userName);

        Task<IEnumerable<DropDownListItem<long>>> GetSelectionAsync(string searchEmail = "");

        /// <summary>
        /// To get current user information. It will work on user role switch mode and normal mode
        /// </summary>
        /// <param name="curentUser"></param>
        /// <returns></returns>
        Task<UserProfileViewModel> GetAsync(IdentityInfo curentUser);

        //Task<IEnumerable<UserProfileViewModel>> GetUsersByOrganizationIdAsync(long id);

        Task<UserProfileViewModel> GetByUsernameAsync(string username);

        Task<UserProfileViewModel> CreateUserAsync(UserProfileViewModel model);

        Task<bool> CheckExistsUser(string email);

        Task<UserProfileViewModel> UpdateCurrentUserAsync(UserProfileViewModel viewModel, string username);

        Task<UserProfileViewModel> UpdateAsync(long id, UserProfileViewModel viewModel, string username);

        Task<UserProfileViewModel> UpdateUserStatusAsync(long id, UpdateUserStatusViewModel viewModel, string username);

        Task UpdateLastSignInDateAsync(string username);

        Task ActivateUserAsync(string username);

        Task SendActivationEmail(long id);
    }
}
