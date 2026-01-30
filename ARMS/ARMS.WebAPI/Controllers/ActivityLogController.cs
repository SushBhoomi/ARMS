using ARMS.Application.Authorization;
using ARMS.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly IActivitiesService _activityservice;
        public ActivityLogController(IActivitiesService activities)
        {
            _activityservice = activities;     
        }

        [HttpGet]
        [Route("GetActivityLog")]
        [AppAuthorize(AppPermissions.Master_Infra_InfraDBType_Add)]
        public async Task<IActionResult> Get(int id,string username = null)
        {

            var result = await _activityservice.GetActivityLog(id,username);
           
            return new JsonResult(result);
        }
    }
}
