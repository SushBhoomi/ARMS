using ARMS.Application.Common;
using ARMS.Application.Entities.Permissions.ViewModels;
using ARMS.Application.Entities.Users.Validations;
using ARMS.Application.Utilities;
using ARMS.Core.Entities;
using ARMS.Core.Models;
using FluentValidation;

namespace ARMS.Application.Entities.Users.ViewModels
{
    public class UserProfileViewModel : ViewModelBase<UserProfileModel>
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        public string Phone { get; set; }

        public string ProfilePicture { get; set; }

        public UserStatus Status { get; set; }

        public DateTime LastSignInDate { get; set; }

        public string StatusName => EnumHelper<UserStatus>.GetDisplayName(this.Status);

        public ICollection<UserRoleViewModel> UserRoles { get; set; }

        public RoleViewModel Role => UserRoles?.Select(ur => ur.Role).FirstOrDefault();

        public ICollection<PermissionViewModel> Permissions { get; set; }

        /// <summary>
        /// To define if current user is in switch mode (pretending to external user role)
        /// </summary
        public string DateFormat { get; set; }

        public override void ValidateAndThrow(bool isUpdating = false)
        {
            new UserProfileValidation().ValidateAndThrow(this);
        }

    }
}
