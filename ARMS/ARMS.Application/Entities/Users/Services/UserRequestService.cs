using ARMS.Application.Common;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Application.Exceptions;
using ARMS.Application.Interfaces.Repository;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Entities;
using ARMS.Core.Entities.QueryModels;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ARMS.Application.Entities.Users.Services
{
    public class UserRequestService : ServiceBase<UserRequestModel, UserRequestViewModel>, IUserRequestService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly AppConfig _appConfig;

        public UserRequestService(IUnitOfWorkProvider uow, IOptions<AppConfig> appConfig) : base(uow)
        {
            _userProfileRepository = (IUserProfileRepository)UnitOfWork.GetRepository<UserProfileModel>();
            _appConfig = appConfig.Value;
        }

        protected override IDictionary<string, string> SortMap { get; } = new Dictionary<string, string>() {
            { "userType", "isInternal" },
            { "statusName", "status"},
            { "CreatedDateOnly", "CreatedDate"}
        };

        public async Task<UserRequestViewModel> GetAsync(long id)
        {
            var model = await this.Repository.GetAsync(x => x.Id == id);
            var result = Mapper.Map<UserRequestViewModel>(model);

            return result;
        }

        public async Task<string> ApproveAsync(long id, UserRequestViewModel viewModel, string currentUser)
        {
            viewModel.ValidateAndThrow();

            var model = await this._userProfileRepository.GetAsync(x => x.Id == id, null, x => x.Include(m => m.UserRoles));

            if ((model.Status != UserStatus.Pending
                && model.Status != UserStatus.Rejected)
                || !viewModel.RoleId.HasValue)
            {
                throw new AppException("msg.cannotApprove");
            }



            model.Status = UserStatus.Active;
            model.Name = viewModel.Name;
            model.Phone = viewModel.Phone;

            if (model.UserRoles.All(ur => ur.RoleId != viewModel.RoleId.Value))
            {
                // treat as 1 user has only 1 role
                // remove old roles
                model.UserRoles.Clear();
                // add new role
                model.UserRoles.Add(new UserRoleModel()
                {
                    UserId = model.Id,
                    RoleId = viewModel.RoleId.Value
                });
            }

            model.Audit(currentUser);

            // Send approval email base on model.IsInternal
            //var emailModel = new UserProfileEmailViewModel() { Name = model.Name };

            //if (model.IsInternal)
            //{
            //    emailModel.ClientLoginUrl = $"{_appConfig.ClientUrl}/login?type=in";
            //    BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"Account Registration Approved (Internal User) {viewModel.Email}",
            //        "AccountRegistrationApproved_InternalUser", emailModel, model.Username,
            //        $"Shipment Portal: Email App Account Registration Approved (Internal User)"));
            //}
            //else
            //{
            //    emailModel.ClientLoginUrl = $"{_appConfig.ClientUrl}/login";
            //    BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"Account Registration Approved (External User) {viewModel.Email}",
            //        "AccountRegistrationApproved_ExternalUser", emailModel, model.Username,
            //        $"Shipment Portal: Email App Account Registration Approved (External User)"));
            //}

            UnitOfWork.SaveChanges();
            return model.Username;
        }

        public async Task<string> RejectAsync(long id, UserRequestViewModel viewModel, string currentUser)
        {
            var model = await this._userProfileRepository.GetAsync(x => x.Id == id);

            if (model.Status != UserStatus.Pending)
            {
                throw new AppException("msg.cannotReject");
            }

            model.Status = UserStatus.Rejected;
            model.Audit(currentUser);

            // Send rejection email base on model.IsInternal
            var user = Mapper.Map<UserProfileViewModel>(model);
            //if (model.IsInternal)
            //{
            //    BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"Account Registration Rejected (Internal User) {viewModel.Email}",
            //        "AccountRegistrationRejected_InternalUser", new AccountRegistrationRejectedEmailTemplateViewModel()
            //        {
            //            Name = user.Name,
            //            SupportEmail = _appConfig.SupportEmail
            //        }, model.Username, $"Shipment Portal: Email App Account Registration Rejected (Internal User)"));
            //}
            //else
            //{
            //    BackgroundJob.Enqueue<SendMailBackgroundJobs>(x => x.SendMailAsync($"Account Registration Rejected (External User) {viewModel.Email}",
            //        "AccountRegistrationRejected_ExternalUser", new AccountRegistrationRejectedEmailTemplateViewModel
            //        {
            //            Name = user.Name,
            //            CompanyName = user.CompanyName,
            //            SupportEmail = _appConfig.SupportEmail
            //        }, model.Username, $"Shipment Portal: Email App Account Registration Rejected (External User)"));
            //}

            UnitOfWork.SaveChanges();
            return model.Username;
        }
    }

}
