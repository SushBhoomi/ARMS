using ARMS.Application.Common;
using ARMS.Application.Entities.Permissions.ViewModels;
using ARMS.Core.Entities;

namespace ARMS.Application.Entities.Permissions.Mappers
{
    public class PermissionMappingProfile : MappingProfileBase<PermissionModel, PermissionViewModel>
    {
        public PermissionMappingProfile()
        {
            CreateMap<PermissionModel, PermissionViewModel>();
        }
    }
}
