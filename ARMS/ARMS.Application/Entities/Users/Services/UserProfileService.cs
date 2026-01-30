using ARMS.Application.Common;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Application.Exceptions;
using ARMS.Application.Interfaces.Repository;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Data;
using ARMS.Core.Entities;
using ARMS.Core.Entities.QueryModels;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace ARMS.Application.Entities.Users.Services
{
    public class UserProfileService : ServiceBase<UserProfileModel, UserProfileViewModel>, IUserProfileService
    {
        private readonly AppConfig _appConfig;
        private readonly IPermissionService _permissionService;
        private readonly IDataQuery _dataQuery;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(IUnitOfWorkProvider uow,
            IOptions<AppConfig> appConfig,
            IPermissionService permissionService,
            IDataQuery dataQuery,
            IRoleRepository roleRepository,
            ILogger<UserProfileService> logger
            ) : base(uow)
        {
            _appConfig = appConfig.Value;
            _permissionService = permissionService;
            _dataQuery = dataQuery;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        protected override IDictionary<string, string> SortMap { get; } = new Dictionary<string, string>() {
            { "statusName", "status"},
            { "organizationTypeName", "organizationType" }
        };

        protected override Func<IQueryable<UserProfileModel>, IQueryable<UserProfileModel>> FullIncludeProperties
        {
            get
            {
                return x => x.Include(m => m.UserRoles).ThenInclude(m => m.Role);
            }
        }

        //public async Task<IEnumerable<UserProfileViewModel>> GetUsersByOrganizationIdAsync(long id)
        //{
        //    var models = await this.Repository.Query(x => (x.Status == UserStatus.Active || x.Status == UserStatus.Inactive || x.Status == UserStatus.WaitForConfirm),
        //        null,
        //        FullIncludeProperties).ToListAsync();
        //    return Mapper.Map<IEnumerable<UserProfileViewModel>>(models);
        //}

        public async Task<UserProfileViewModel> GetByUsernameAsync(string username)
        {
            var model = await Repository.GetAsNoTrackingAsync(x => x.Username == (username ?? ""));
            if (model == null)
            {
                throw new AppEntityNotFoundException($"Object not found!");
            }
            return Mapper.Map<UserProfileViewModel>(model);
        }

        public async Task<UserProfileViewModel> GetAsync(string userName)
        {
            var model = await this.Repository.GetAsync(x => x.Username == userName, null, FullIncludeProperties);
            var viewModel = Mapper.Map<UserProfileViewModel>(model);

            return viewModel;
        }

        public async Task<UserProfileViewModel> GetAsync(IdentityInfo currentUser)
        {
            var userName = currentUser.Username;

            // Normal mode, get data by username/email

            var model = await Repository.GetAsync(x => x.Username == userName, null, FullIncludeProperties);
            var viewModel = Mapper.Map<UserProfileViewModel>(model);
            return viewModel;
        }

        public async Task<UserProfileViewModel> GetAsync(long id)
        {
            var model = await this.Repository.GetAsync(x => x.Id == id, null, FullIncludeProperties);
            var viewModel = Mapper.Map<UserProfileViewModel>(model);

            return viewModel;
        }

        public async Task<IEnumerable<DropDownListItem<long>>> GetSelectionAsync(string searchEmail = "")
        {
            var query = Repository.QueryAsNoTracking(x => x.Status == UserStatus.Active);

            if (!string.IsNullOrWhiteSpace(searchEmail))
            {
                query = query.Where(x => x.Username.Contains(searchEmail));
            }

            var users = await query.Select(x => new DropDownListItem<long>
            {
                Text = x.Username,
                Value = x.Id
            }).ToListAsync();

            return users ?? new List<DropDownListItem<long>>();
        }

        public async Task<UserProfileViewModel> CreateUserAsync(UserProfileViewModel model)
        {
            var user = new UserProfileModel
            {
                AccountNumber = Guid.NewGuid().ToString().ToUpper().Substring(0, AppConstant.ACCOUNT_NUMBER_LENGTH),
                Username = model.Username,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Status = UserStatus.Pending,
                LastSignInDate = DateTime.UtcNow,
                UserRoles = new List<UserRoleModel>
                {
                    new UserRoleModel
                    {
                        UserId = model.Id,
                        RoleId = (long) (Role.Pending )
                    }
                }
            };

            user.Audit();

            // Create app account
            await Repository.AddAsync(user);
            await this.UnitOfWork.SaveChangesAsync();

            //BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"Account Registration Wait-For-Approve (User) {user.Email}",
            //    "AccountRegistrationWaitForApprove_User",
            //     new AccountRegistrationEmailTemplateViewModel()
            //     {
            //         Name = model.Name,
            //         SupportEmail = _appConfig.SupportEmail
            //     },
            //     model.Username, $" Shipment Portal: Email App Account Registration Wait-For-Approve (User)"));


            return Mapper.Map<UserProfileViewModel>(user);
        }

        /// <summary>
        /// Add new an external user 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>



        public async Task<UserProfileViewModel> UpdateUserStatusAsync(long id, UpdateUserStatusViewModel viewModel, string username)
        {
            var userProfile = await Repository.GetAsync(x => x.Id == id);

            if (userProfile == null)
            {
                throw new AppEntityNotFoundException($"Object with Id {id} not found!");
            }

            userProfile.Status = viewModel.Status;
            userProfile.Audit(username);

            Repository.Update(userProfile);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserProfileViewModel>(userProfile);
        }

        public async Task<bool> CheckExistsUser(string email)
        {
            var user = await this.Repository.GetAsync(x => x.Email == email);
            return user != null;
        }



        public async Task UpdateLastSignInDateAsync(string username)
        {
            try
            {
                var userList = await this.Repository.Query(x => x.Username == username).ToListAsync();
                foreach (var user in userList)
                {
                    user.LastSignInDate = DateTime.UtcNow;
                }
                await this.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AppException(ex.ToString());
            }
        }



        public override async Task<DataSourceResult> ListAsync(DataSourceRequest request)
        {
            CompositeFilterDescriptor descriptor = new CompositeFilterDescriptor()
            {
                FilterDescriptors = new FilterDescriptorCollection
                {
                    new FilterDescriptor("status", FilterOperator.IsEqualTo, UserStatus.Active),
                    new FilterDescriptor("status", FilterOperator.IsEqualTo, UserStatus.Inactive),
                    new FilterDescriptor("status", FilterOperator.IsEqualTo, UserStatus.WaitForConfirm)
                },
                LogicalOperator = FilterCompositionLogicalOperator.Or
            };
            if (request.Filters == null)
            {
                request.Filters = new FilterDescriptorCollection();
            }
            request.Filters.Add(descriptor);

            IQueryable<UserListQueryModel> query;
            string sql =
                        @"SELECT
                            USP.Id,
	                        USP.AccountNumber,
	                        USP.Email,
	                        USP.[Name],
	                        USP.LastSignInDate,
	                        USP.[Status],
							CASE WHEN USP.[Status] = -1 THEN 'label.deleted'
								 WHEN USP.[Status] = 0 THEN 'label.rejected'
								 WHEN USP.[Status] = 1 THEN 'label.pending'
								 WHEN USP.[Status] = 2 THEN 'label.active'
								 WHEN USP.[Status] = 3 THEN 'label.inactive'
								 WHEN USP.[Status] = 4 THEN 'label.waitForConfirm'
							ELSE '' END AS StatusName,
	                        IIF(T.[Name] IS NULL, '', T.[Name]) AS RoleName
                        FROM UserProfiles USP (NOLOCK)
                        OUTER APPLY (
	                        SELECT TOP 1 R.*
	                        FROM UserRoles USR (NOLOCK) INNER JOIN Roles R (NOLOCK) ON USR.RoleId = R.Id
	                        WHERE USR.UserId = USP.Id
						) T
                   ";

            query = _dataQuery.GetQueryable<UserListQueryModel>(sql);

            var data = await query.ToDataSourceResultAsync(request);
            return data;
        }

        public async Task<UserProfileViewModel> UpdateCurrentUserAsync(UserProfileViewModel viewModel, string username)
        {
            try
            {
                viewModel.ValidateAndThrow();
                var currentModel = await this.Repository.GetAsync(x => x.Username == username);
                currentModel.Audit(username);
                currentModel.Name = viewModel.Name;
                currentModel.Title = viewModel.Title;
                currentModel.Department = viewModel.Department;
                currentModel.ProfilePicture = viewModel.ProfilePicture;
                currentModel.Phone = viewModel.Phone;
                await this.UnitOfWork.SaveChangesAsync();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.ToString());
            }
        }

        public async Task<UserProfileViewModel> UpdateAsync(long id, UserProfileViewModel viewModel, string username)
        {
            try
            {
                UnitOfWork.BeginTransaction();

                viewModel.ValidateAndThrow();
                var model = await Repository.GetAsync(x => x.Id == id, null, FullIncludeProperties);
                var role = model.UserRoles?.Select(ur => ur.Role).FirstOrDefault();
                var isChangeRole = role != null && role.Id != viewModel.Role.Id;

                // update UserRoles
                if (viewModel.Role != null)
                {
                    var userRole = model.UserRoles?.FirstOrDefault();
                    if (userRole != null)
                    {
                        model.UserRoles.Remove(userRole);
                    }
                    await UnitOfWork.SaveChangesAsync();
                    userRole = new UserRoleModel
                    {
                        UserId = model.Id,
                        RoleId = viewModel.Role.Id
                    };
                    userRole.Audit(username);
                    model.UserRoles?.Add(userRole);
                }

                model.Name = viewModel.Name;
                model.Phone = viewModel.Phone;
                model.Title = viewModel.Title;
                model.Department = viewModel.Department;
                model.Status = viewModel.Status;
                model.DateFormat = viewModel.DateFormat;

                model.Audit(username);
                this.Repository.Update(model);
                await this.UnitOfWork.SaveChangesAsync();

                //if (isChangeRole)
                //{
                //    await _permissionService.InvalidatePermissionCache(username);
                //    BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"Your role in Shipment Portal has been changed {model.Email}",
                //        "EmailUserRoleChanged", new UserRoleChangedEmailTemplateViewModel()
                //        {
                //            Name = viewModel.Name,
                //            RoleName = viewModel.Role.Name,
                //            SupportEmail = _appConfig.SupportEmail
                //        }, model.Username, $"Shipment Portal: Your role in Shipment Portal has been changed"));
                //}

                UnitOfWork.CommitTransaction();
                return Mapper.Map<UserProfileModel, UserProfileViewModel>(model);
            }
            catch
            {
                UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task ActivateUserAsync(string username)
        {
            var user = await this.Repository.GetAsync(x => x.Username == username);

            if (user.Status == UserStatus.WaitForConfirm)
            {
                user.Status = UserStatus.Active;
            }

            await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task SendActivationEmail(long id)
        {
            var user = await Repository.GetAsNoTrackingAsync(x => x.Id == id);

            if (user == null)
            {
                throw new AppEntityNotFoundException($"Object with id {id} not found!");
            }

            //string token = BuildIdToken(user.Username);
            //string activationLink = BuildAccountActivateUrl(token);
            //BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"New Account is Created {user.Email}",
            //    "AccountCreation_Activation_ExternalUser", new AccountCreationEmailTemplateViewModel()
            //    {
            //        ActivationLink = activationLink,
            //        ForgotPasswordLink = ForgotPasswordUrl,
            //        ActivationExpireIn = _appConfig.B2C.PolicyTokenLifeTime,
            //    },
            //    user.Username, $" Shipment Portal: Email App New Account is Created")
            //);
        }

    }

}
