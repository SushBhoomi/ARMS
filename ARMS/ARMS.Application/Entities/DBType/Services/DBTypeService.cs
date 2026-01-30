using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.Services.Interfece;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Exceptions;
using ARMS.Application.Interfaces.Repository;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.DBType.Services
{
    public class DBTypeService : ServiceBase<DBTypeMasterModel, DBTypeViewModel>, IDBTypeService
    {
        IPermissionService _permissionService;
        IDBTypeRepository _IdbtypeRepository;

        public DBTypeService(IDBTypeRepository IDBType, IPermissionService permissionService, IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
            _permissionService = permissionService;
            _IdbtypeRepository = IDBType;
        }

        public async Task<DBTypeViewModel> CreateDBTypeAsync(DBTypeViewModel viewModel, string username)
        {
            viewModel.ValidateAndThrow();

            var DbType = new DBTypeMasterModel
            {
                DBTypeName = viewModel.DBTypeName,
                IsActive = viewModel.IsActive

            };
            DbType.Audit(username);

            // Create app account
            await Repository.AddAsync(DbType);
            await UnitOfWork.SaveChangesAsync();
            OnEntityCreated(DbType);
            return Mapper.Map<DBTypeViewModel>(DbType);
        }

        public override async Task<IEnumerable<DBTypeViewModel>> GetAllAsync()
        {
            var models = await Repository.Query().ToListAsync();
            return Mapper.Map<IEnumerable<DBTypeViewModel>>(models);
        }

        public async Task<IEnumerable<DBTypeViewModel>> Search(string dbTypeName, string isActive)
        {
            var models = await _IdbtypeRepository.AsQueryable(dbTypeName, isActive).ToListAsync();
            return Mapper.Map<IEnumerable<DBTypeViewModel>>(models);
        }

        public async Task<DBTypeViewModel> UpdateAsync(DBTypeViewModel viewModel, string username, params object[] keys)
        {


            viewModel.ValidateAndThrow(true);

            DBTypeMasterModel model = await Repository.FindAsync(keys);

            if (model == null)
            {
                throw new AppEntityNotFoundException($"Object with the id {string.Join(", ", keys)} not found!");
            }

            Mapper.Map(viewModel, model);

            var error = await ValidateDatabaseBeforeAddOrUpdateAsync(model);
            if (!string.IsNullOrEmpty(error))
            {
                throw new AppException(error);
            }
            model.Audit(username);
            Repository.Update(model);
            await UnitOfWork.SaveChangesAsync();
            OnEntityUpdated(model);


            viewModel = Mapper.Map<DBTypeViewModel>(model);
            return viewModel;
        }

    }
}
