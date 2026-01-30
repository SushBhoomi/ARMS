using ARMS.Application.Common;
using ARMS.Core.Entities;

namespace ARMS.Application.Entities.Permissions.ViewModels
{
    public class PermissionViewModel : ViewModelBase<PermissionModel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public override void ValidateAndThrow(bool isUpdating = false)
        {

        }
    }
}
