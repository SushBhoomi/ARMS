using ARMS.Application.Common;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities;
using ARMS.Core.Entities.QueryModels;

namespace ARMS.Application.Entities.Users.Mappers
{
    public class UserMappingProfile : MappingProfileBase<UserProfileModel, UserProfileViewModel>
    {
        public UserMappingProfile()
        {
            //UserRequest
            CreateMap<UserRequestModel, UserRequestViewModel>()
                .ForMember(opts => opts.CreatedDate, dest => dest.MapFrom(src => src.CreatedDate.Date));
            CreateMap<UserRequestViewModel, UserRequestModel>();

            //Role
            CreateMap<RoleModel, RoleViewModel>()
               .ForMember(s => s.PermissionIds, d => d.MapFrom(m => m.RolePermissions.Select(x => x.PermissionId)));

            //UserRole
            CreateMap<UserRoleModel, UserRoleViewModel>().ReverseMap();

            //UserProfile
            CreateMap<UserProfileModel, UserProfileViewModel>().ForMember(x => x.Permissions, x => x.Ignore()).ReverseMap();


        }
    }
}
