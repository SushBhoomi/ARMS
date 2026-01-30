using ARMS.Application.Authorization;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.ExceptionLog.Services.Interface;
using ARMS.Application.Entities.ExceptionLog.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        private readonly IExceptionLog _service;

        public ErrorLogController(IExceptionLog service)
        {
                _service = service;
        }

        [HttpPost]
        [Route("Insert")]
        
        public async Task<IActionResult> Post([FromBody] ExceptionLogViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _service.CreateAsync(model);
                return new JsonResult(result);
            }
            return BadRequest();
        }
    }
}
