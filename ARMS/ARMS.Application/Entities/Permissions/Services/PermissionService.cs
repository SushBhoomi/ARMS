using ARMS.Application.Authorization;
using ARMS.Application.Caching;
using ARMS.Application.Common;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Entities.Permissions.ViewModels;
using ARMS.Application.Interfaces.Repository;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Entities;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ARMS.Application.Entities.Permissions.Services
{
    public class PermissionService : ServiceBase<PermissionModel, PermissionViewModel>, IPermissionService
    {
        private readonly ICacheService _cacheService;
        private readonly IRepository<UserProfileModel> _userProfileRepository;
        private readonly IDictionary<string, List<PermissionViewModel>> _userPermissions;

        public PermissionService(IUnitOfWorkProvider unitOfWorkProvider,
            IRepository<UserProfileModel> userProfileRepository,
            ICacheService cacheService)
           : base(unitOfWorkProvider)
        {
            _userProfileRepository = userProfileRepository;
            _cacheService = cacheService;
            _userPermissions = _cacheService.Get<IDictionary<string, List<PermissionViewModel>>>(AppConstant.DEFAULT_PERMISSION_CACHE)
                               ?? new Dictionary<string, List<PermissionViewModel>>();
        }

        #region Working with username/email

        public async Task<bool> IsUserGrantedAsync(string userName, string permissionName)
        {
            if (!_userPermissions.TryGetValue(userName, out var permissions))
            {
                permissions = (await GetUserPermissions(userName)).ToList();
            }
            return permissions.Any(s => s.Name == permissionName);
        }

        public async Task<bool> IsUserGrantedAsync(bool requiresAll, string userName, params string[] permissionNames)
        {
            if (permissionNames == null || !permissionNames.Any())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionName in permissionNames)
                {
                    if (!(await IsUserGrantedAsync(userName, permissionName)))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    if (await IsUserGrantedAsync(userName, permissionName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public async Task<IEnumerable<PermissionViewModel>> GetUserPermissions(string userName)
        {
            if (!_userPermissions.TryGetValue(userName, out var permissions))
            {
                permissions = (await InvalidatePermissionCache(userName)).ToList();
            }
            return permissions;
        }

        public async Task<IEnumerable<PermissionViewModel>> InvalidatePermissionCache(string username)
        {
            // Step 1: Get the user with their roles
            var user = await _userProfileRepository.GetListQueryable()
                .Where(u => u.Username == username)
                .Select(u => new
                {
                    RoleIds = u.UserRoles
                        .Where(ur => ur.Role.Status == RoleStatus.Active)
                        .Select(ur => ur.RoleId)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            var permissions = new List<PermissionViewModel>();

            if (user != null && user.RoleIds.Any())
            {
                // Step 2: Get permissions for those roles
                var dbPermissions = await Repository.GetListQueryable()
                    .Where(p => p.RolePermissions.Any(rp => user.RoleIds.Contains(rp.RoleId)))
                    .ToListAsync();

                permissions = Mapper.Map<IEnumerable<PermissionViewModel>>(dbPermissions).ToList();
            }

            // Step 3: Update cache
            if (_userPermissions.ContainsKey(username))
            {
                _userPermissions.Remove(username);
            }
            _userPermissions.Add(username, permissions);
            _cacheService.Set(AppConstant.DEFAULT_PERMISSION_CACHE, _userPermissions);

            return permissions;
        }

        #endregion Working with username/email

        public override async Task<IEnumerable<PermissionViewModel>> GetAllAsync()
        {
            var models = await this.Repository.Query(null, p => p.OrderBy(x => x.Order)).ToListAsync();
            return Mapper.Map<IEnumerable<PermissionViewModel>>(models);
        }

        public IEnumerable<PermissionViewModel> GeAllStaticPermissions()
        {
            var createdDate = new DateTime(2019, 01, 01);
            var appPermissions = typeof(AppPermissions)
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            var permissionModels = new List<PermissionViewModel>();
            for (int i = 0; i < appPermissions.Count(); i++)
            {
                permissionModels.Add(new PermissionViewModel
                {
                    Id = i + 1,
                    Name = appPermissions[i].GetRawConstantValue() as string,
                    CreatedBy = AppConstant.SYSTEM_USERNAME,
                    CreatedDate = createdDate
                });
            }
            return permissionModels;
        }

        public void RemovePermissionsCacheOfUser(string username)
        {
            _userPermissions.Remove(username);
        }

        public void RemoveAllPermissionsCacheOfAllUser()
        {
            _userPermissions.Clear();
        }

        #region Working with role. It is user role switch mode

        //public async Task<bool> IsRoleGrantedAsync(long roleId, string permissionName)
        //{
        //    var key = roleId.ToString();
        //    if (!_userPermissions.TryGetValue(key, out var permissions))
        //    {
        //        permissions = (await GetRolePermissions(roleId)).ToList();
        //    }
        //    return permissions.Any(s => s.Name == permissionName);
        //}

        //public async Task<bool> IsRoleGrantedAsync(bool requiresAll, long roleId, params string[] permissionNames)
        //{
        //    if (permissionNames == null || !permissionNames.Any())
        //    {
        //        return true;
        //    }

        //    if (requiresAll)
        //    {
        //        foreach (var permissionName in permissionNames)
        //        {
        //            if (!await IsRoleGrantedAsync(roleId, permissionName))
        //            {
        //                return false;
        //            }
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        foreach (var permissionName in permissionNames)
        //        {
        //            if (await IsRoleGrantedAsync(roleId, permissionName))
        //            {
        //                return true;
        //            }
        //        }

        //        return false;
        //    }
        //}

        //public async Task<IEnumerable<PermissionViewModel>> GetRolePermissions(long roleId)
        //{
        //    var key = roleId.ToString();
        //    if (!_userPermissions.TryGetValue(key, out var permissions))
        //    {
        //        permissions = (await InvalidatePermissionCache(roleId)).ToList();
        //    }
        //    return permissions;
        //}

        //public async Task<IEnumerable<PermissionViewModel>> InvalidatePermissionCache(long roleId)
        //{
        //    var rolesOfUser = new List<long>() { roleId };
        //    var permissions = new List<PermissionViewModel>();
        //    if (rolesOfUser != null)
        //    {
        //        var dbPermissions = await Repository.Query(p => p.RolePermissions.Any(rp => rolesOfUser.Contains(rp.RoleId))).ToListAsync();
        //        permissions = Mapper.Map<IEnumerable<PermissionViewModel>>(dbPermissions).ToList();
        //    }

        //    var key = roleId.ToString();
        //    if (_userPermissions.ContainsKey(key))
        //    {
        //        _userPermissions.Remove(key);
        //    }
        //    _userPermissions.Add(key, permissions);
        //    _cacheService.Set(AppConstant.DEFAULT_PERMISSION_CACHE, _userPermissions);
        //    return permissions;
        //}

        #endregion #region Working with role. It is user role switch mode

    }

}
