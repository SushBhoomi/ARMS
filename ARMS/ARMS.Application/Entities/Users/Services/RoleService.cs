using ARMS.Application.Common;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Application.Exceptions;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Entities;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ARMS.Application.Entities.Users.Services
{
    public class RoleService : ServiceBase<RoleModel, RoleViewModel>, IRoleService
    {
        private readonly IPermissionService _permissionService;
        public RoleService(IPermissionService permissionService, IUnitOfWorkProvider uow) : base(uow)
        {
            _permissionService = permissionService;
        }

        public async Task<RoleViewModel> GetAsync(long id)
        {
            var model = await this.Repository.GetAsync(x => x.Id == id, null, FullIncludeProperties);
            var viewModel = Mapper.Map<RoleViewModel>(model);
            return viewModel;
        }

        public override async Task<IEnumerable<RoleViewModel>> GetAllAsync()
        {
            var models = await this.Repository.Query(x => x.Status == RoleStatus.Active).ToListAsync();
            return Mapper.Map<IEnumerable<RoleViewModel>>(models);
        }


        protected override Func<IQueryable<RoleModel>, IQueryable<RoleModel>> FullIncludeProperties
        {
            get
            {
                return x => x.Include(m => m.RolePermissions);
            }
        }

        public async Task<RoleViewModel> UpdateAsync(RoleViewModel viewModel, string userName)
        {
            try
            {
                viewModel.ValidateAndThrow();

                var model = await Repository.GetAsync(x => x.Id == viewModel.Id, null, FullIncludeProperties);
                model.Description = viewModel.Description;

                var deletedList = model.RolePermissions?.Where(x => !viewModel.PermissionIds.Any(y => y == x.PermissionId)).ToList();
                foreach (var item in deletedList)
                {
                    model.RolePermissions.Remove(item);
                }

                var addedIdList = viewModel.PermissionIds?.Where(x => !model.RolePermissions.Any(y => y.PermissionId == x));
                foreach (var id in addedIdList)
                {
                    var rolePermissionModel = new RolePermissionModel();
                    rolePermissionModel.RoleId = model.Id;
                    rolePermissionModel.PermissionId = id;
                    rolePermissionModel.Audit(userName);
                    model.RolePermissions.Add(rolePermissionModel);
                }
                model.Audit(userName);

                this.Repository.Update(model);
                await this.UnitOfWork.SaveChangesAsync();
                _permissionService.RemoveAllPermissionsCacheOfAllUser();
                return Mapper.Map<RoleModel, RoleViewModel>(model);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
