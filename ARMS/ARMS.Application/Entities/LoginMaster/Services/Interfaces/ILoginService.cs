using ARMS.Application.Common;
using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.LoginMaster.Services.Interfaces
{
    public interface ILoginService : IServiceBase<LoginMasterModel, LoginViewModel>
    {
        Task<LoginViewModel> GetAsync(string userName , string password);
        Task<LoginViewModel> GetUserAsync(string userName);
    }
}
