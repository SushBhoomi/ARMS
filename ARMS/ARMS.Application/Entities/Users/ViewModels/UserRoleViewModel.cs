using ARMS.Application.Common;
using ARMS.Core.Entities;

namespace ARMS.Application.Entities.Users.ViewModels
{
    public class UserRoleViewModel : ViewModelBase<UserRoleModel>
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }

        public RoleViewModel Role { get; set; }

        public override void ValidateAndThrow(bool isUpdating = false)
        {
            throw new NotImplementedException();
        }
    }

}
