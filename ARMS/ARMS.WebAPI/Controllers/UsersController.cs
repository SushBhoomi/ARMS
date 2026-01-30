using ARMS.Application.Authorization;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Data;
using ARMS.Core.Models;
using ARMS.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IPermissionService _permissionService;


        public UsersController(
            IUserProfileService userProfileService,
            IPermissionService permissionService)
        {
            _userProfileService = userProfileService;
            _permissionService = permissionService;
        }

        [Route("{id}")]
        [HttpGet]
        [AppAuthorize(AppPermissions.User_UserDetail)]
        public async Task<IActionResult> Get(long id)
        {
            var viewModel = await _userProfileService.GetAsync(id);
            return new JsonResult(viewModel);
        }

        [Route("Current")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Get current user by claim
            var userName = CurrentUser.Username;
            
            if (string.IsNullOrWhiteSpace(userName))
                return new JsonResult(null);

            var viewModel = await _userProfileService.GetAsync(CurrentUser);
            if (viewModel != null)
            {
                if (viewModel.Status == UserStatus.WaitForConfirm)
                {
                    await _userProfileService.ActivateUserAsync(viewModel.Username);
                }
                viewModel.Permissions = (await _permissionService.GetUserPermissions(CurrentUser.Username)).ToList();
                return new JsonResult(viewModel);
            }

            // If not existed in DB, create new profile with claim info
            var user = new UserProfileViewModel()
            {
                Username = userName,
                Email = CurrentUser.Email,
                Name = CurrentUser.Name,
                Phone = CurrentUser.Phone
            };
            viewModel = await _userProfileService.CreateUserAsync(user);
            viewModel.Permissions = (await _permissionService.GetUserPermissions(CurrentUser.Username)).ToList();

            return new JsonResult(viewModel);
        }

        //[Route("ByOrganization/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> GetUsersByOrganizationId(long id)
        //{
        //    var result = await _userProfileService.GetUsersByOrganizationIdAsync(id);
        //    return new JsonResult(result);
        //}

        [Route("CheckExistsUser")]
        [HttpGet]
        public async Task<IActionResult> CheckExistsUser(string email)
        {
            var result = await _userProfileService.CheckExistsUser(email);
            return new JsonResult(result);
        }

        [HttpPut]
        [Route("{id}/Status")]
        [AppAuthorize]
        public async Task<IActionResult> UpdateUserStatusAsync(long id, [FromBody] UpdateUserStatusViewModel viewModel)
        {
            var result = await _userProfileService.UpdateUserStatusAsync(id, viewModel, CurrentUser.Username);
            return Ok(result);
        }


        [HttpPost]
        [Route("Create")]
        [AppAuthorize]
        public async Task<IActionResult> Post([FromBody] UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userProfileService.CreateUserAsync(model);
                return new JsonResult(result);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("Current")]
        [AppAuthorize]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var username = CurrentUser.Username;
                var result = await _userProfileService.UpdateCurrentUserAsync(viewModel, username);
                return new JsonResult(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("{id}")]
        [AppAuthorize]
        public async Task<IActionResult> Update(long id, [FromBody] UserProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var username = CurrentUser.Username;
                var result = await _userProfileService.UpdateAsync(id, viewModel, username);
                return new JsonResult(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Search")]
        [AppAuthorize(AppPermissions.User_UserList)]
        public async Task<IActionResult> Search([DataSourceRequest] DataSourceRequest request)
        {
            var viewModels = await _userProfileService.ListAsync(request);
            return new JsonResult(viewModels);
        }

        [HttpGet]
        [Route("Selections")]
        [AppAuthorize(AppPermissions.User_UserList)]
        public async Task<IActionResult> GetMultipleSelectionAsync([FromQuery] string searchEmail = "")
        {
            var viewModels = await _userProfileService.GetSelectionAsync(searchEmail);
            return new JsonResult(viewModels);
        }

        [HttpPut]
        [Route("current/lastSignIn")]
        public async Task<IActionResult> UpdateLastSignInDate()
        {
            var username = CurrentUser.Username;
            await _userProfileService.UpdateLastSignInDateAsync(username);
            return Ok();
        }


        [HttpPost]
        [Route("HasPermissions")]
        [AppAuthorize]
        public async Task<IActionResult> HasPermissions()
        {
            return Ok();
        }

        [HttpGet]
        [Route("ResetPermissionCache")]
        [AppAuthorize]
        public IActionResult ResetPermissionCache()
        {
            _permissionService.RemovePermissionsCacheOfUser(CurrentUser.Username);
            return Ok();
        }


        [HttpPost]
        [Route("{id}/send-activation-email")]
        [AppAuthorize]
        public async Task<IActionResult> SendActivationEmailAsync(long id)
        {
            await _userProfileService.SendActivationEmail(id);
            return Ok();
        }
    }

}
