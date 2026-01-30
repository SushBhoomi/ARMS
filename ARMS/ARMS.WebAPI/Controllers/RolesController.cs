using ARMS.Application.Authorization;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.Core.Data;
using ARMS.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [AppAuthorize(AppPermissions.Master_Application_DLMaster_List)]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.GetAllAsync();
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AppAuthorize(AppPermissions.Master_Application_DLMaster_List)]

        public async Task<IActionResult> Get(long id)
        {
            var result = await _roleService.GetAsync(id);
            return new JsonResult(result);
        }

        //[HttpGet]
        //[Route("Official")]
        //public async Task<IActionResult> GetOfficial()
        //{
        //    var result = await _roleService.GetOfficialAsync();
        //    return new JsonResult(result);
        //}

        [HttpGet]
        [Route("Search")]
        [AppAuthorize(AppPermissions.User_RoleList)]
        public async Task<IActionResult> Search([DataSourceRequest] DataSourceRequest request)
        {
            var viewModels = await _roleService.ListAsync(request);
            return new JsonResult(viewModels);
        }

        [HttpPut]
        [Route("{id}")]
        [AppAuthorize(AppPermissions.User_RoleDetail_Edit)]
        public async Task<IActionResult> Update(long id, [FromBody] RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var username = CurrentUser.Username;
                var result = await _roleService.UpdateAsync(viewModel, username);
                return new JsonResult(result);
            }
            return BadRequest();
        }
    }
}
