using ARMS.Application.Common;
using ARMS.Application.Entities.Users.ViewModels;
using FluentValidation;

namespace ARMS.Application.Entities.Users.Validations
{
    public class RoleValidation : BaseValidation<RoleViewModel>
    {
        public RoleValidation()
        {
            RuleFor(a => a.Description).MaximumLength(500);
        }
    }
}
