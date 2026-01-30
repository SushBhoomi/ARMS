using ARMS.Application.Common;
using ARMS.Application.Entities.Users.ViewModels;
using FluentValidation;

namespace ARMS.Application.Entities.Users.Validations
{
    public class UserRequestsValidation : BaseValidation<UserRequestViewModel>
    {
        public UserRequestsValidation()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.RoleId).NotNull();
            RuleFor(a => a.Username).NotEmpty();
            RuleFor(a => a.Phone).Length(1, 32).When(x => !string.IsNullOrEmpty(x.Phone));
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
        }
    }
}
