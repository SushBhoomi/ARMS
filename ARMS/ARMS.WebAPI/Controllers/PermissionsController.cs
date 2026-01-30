using ARMS.Application.Entities.Permissions.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> Get()
        {
            var result = await _permissionService.GetAllAsync();
            return new JsonResult(result);
        }
    }
}
