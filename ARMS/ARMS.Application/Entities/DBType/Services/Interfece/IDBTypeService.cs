using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.DBType.Services.Interfece
{
    public interface IDBTypeService : IServiceBase<DBTypeMasterModel, DBTypeViewModel>
    {
        new Task<IEnumerable<DBTypeViewModel>> GetAllAsync();

        Task<DBTypeViewModel> CreateDBTypeAsync(DBTypeViewModel viewModel, string username);
        new Task<DBTypeViewModel> CreateAsync(DBTypeViewModel viewModel);

        Task<DBTypeViewModel> UpdateAsync(DBTypeViewModel viewModel, string username, params object[] keys);

        new Task<bool> DeleteByKeysAsync(params object[] keys);

        Task<IEnumerable<DBTypeViewModel>> Search(string dbTypeName, string isActive);
    }
}
