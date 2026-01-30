using ARMS.Application.Common;
using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Application.Entities.Permissions.ViewModels;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.LoginMaster.Mappers
{
    public class LoginMappingProfile : MappingProfileBase<LoginMasterModel, LoginViewModel>
    {
        public LoginMappingProfile()
        {
            CreateMap<LoginMasterModel, LoginViewModel>();
        }
    }
}
