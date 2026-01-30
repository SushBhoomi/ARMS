using ARMS.Application.Common;
using ARMS.Application.Entities.LoginMaster.Services.Interfaces;
using ARMS.Application.Entities.LoginMaster.ViewModels;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Entities.Users.Services;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Application.Interfaces.Repository;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Data;
using ARMS.Core.Entities;
using ARMS.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.LoginMaster.Services
{
    public class LoginService : ServiceBase<LoginMasterModel, LoginViewModel>, ILoginService
    {
        private readonly IPermissionService _permissionService;
        public LoginService(IPermissionService permissionService, IUnitOfWorkProvider uow) : base(uow)
        {
            _permissionService = permissionService;
        }


        public async Task<LoginViewModel> GetAsync(string userName, string password)
        {
            var user = await this.Repository.GetAsync(x => x.Username == userName && x.Password == password);
            var viewModel = Mapper.Map<LoginViewModel>(user);
            return viewModel;
        }

        public async Task<LoginViewModel> GetUserAsync(string userName)
        {
            var user = await this.Repository.GetAsync(x => x.Username == userName);
            var viewModel = Mapper.Map<LoginViewModel>(user);
            return viewModel;
        }
    }
}
