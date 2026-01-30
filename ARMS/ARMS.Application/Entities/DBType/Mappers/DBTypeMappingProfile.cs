using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.DBType.Mappers
{
    public class DBTypeMappingProfile : MappingProfileBase<DBTypeMasterModel, DBTypeViewModel>
    {
        public DBTypeMappingProfile()
        {
            CreateMap<DBTypeMasterModel, DBTypeViewModel>();
            CreateMap<DBTypeMasterModel, DBTypeViewModel>().ReverseMap();
        }
    }
}
