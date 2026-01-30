using ARMS.Application.Common;
using ARMS.Application.Entities.Users.Validations;
using ARMS.Application.Utilities;
using ARMS.Core.Entities;
using ARMS.Core.Models;
using FluentValidation;

namespace ARMS.Application.Entities.Users.ViewModels
{
    public class RoleViewModel : ViewModelBase<RoleModel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Activated { get; set; }
        public RoleStatus Status { get; set; }
        public string StatusName => EnumHelper<RoleStatus>.GetDisplayName(this.Status);
        //public ICollection<userro> UserProfileRoles { get; set; }
        public ICollection<long> PermissionIds { get; set; }

        public override void ValidateAndThrow(bool isUpdating = false)
        {
            new RoleValidation().ValidateAndThrow(this);
        }
    }
}
