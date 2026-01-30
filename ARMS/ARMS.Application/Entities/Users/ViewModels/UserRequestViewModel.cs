using ARMS.Application.Common;
using ARMS.Application.Entities.Users.Validations;
using ARMS.Application.Utilities;
using ARMS.Core.Entities.QueryModels;
using ARMS.Core.Models;
using FluentValidation;

namespace ARMS.Application.Entities.Users.ViewModels
{
    public class UserRequestViewModel : ViewModelBase<UserRequestModel>
    {
        public string Username { get; set; }

        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public UserStatus Status { get; set; }

        public string StatusName => EnumHelper<UserStatus>.GetDisplayName(this.Status);

        public DateTime CreatedDateOnly => CreatedDate.Date;

        public long? RoleId { get; set; }

        public string RoleName { get; set; }

        public UserRequestViewModel()
        : base()
        { }

        public UserRequestViewModel(UserRequestModel model) : base(model)
        {
            Username = model.Username;
            Email = model.Email;
            Status = model.Status;
            Name = model.Name;
            Phone = model.Phone;
        }

        public override void ValidateAndThrow(bool isUpdating = false)
        {
            new UserRequestsValidation().ValidateAndThrow(this);
        }
    }

}
