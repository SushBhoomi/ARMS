using ARMS.Application.Authorization;
using ARMS.Application.Entities.DBType.Services.Interfece;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.Users.ViewModels;
using ARMS.WebAPI.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using static System.Net.WebRequestMethods;

namespace ARMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBTypeController : ControllerBase
    {
        private readonly IDBTypeService _IDBtypeservice;
        private readonly ILdapAuhenticationService _ldapAuhenticationservice;
        
        public DBTypeController(IDBTypeService IDBTypeservice,ILdapAuhenticationService ldapAuhenticationService)
        {
            _IDBtypeservice = IDBTypeservice;
            _ldapAuhenticationservice = ldapAuhenticationService;
                
            
        }

        [HttpGet]
        [AppAuthorize(AppPermissions.Master_Infra_InfraDBType_List)]
        public async Task<IActionResult> Get()
        {

            var result = await _IDBtypeservice.GetAllAsync();
            //var Activity = $"/api/Infra DBType Master/List";
            //await _ldapAuhenticationservice.CreateActivityLog(Activity, CurrentUser.Username,CurrentUser.Id);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("Insert")]
        [AppAuthorize(AppPermissions.Master_Infra_InfraDBType_Add)]
        public async Task<IActionResult> Post([FromBody] DBTypeViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                
                var result = await _IDBtypeservice.CreateDBTypeAsync(viewModel, CurrentUser.Username);
                //var Activity = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/DBType/Insert";
                //await _ldapAuhenticationservice.CreateActivityLog(Activity, CurrentUser.Username,CurrentUser.Id);
                return new JsonResult(result);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("Edit")]
        [AppAuthorize(AppPermissions.Master_Infra_InfraDBType_Edit)]
        public async Task<IActionResult> Update([FromBody] DBTypeViewModel viewModel, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _IDBtypeservice.UpdateAsync(viewModel, CurrentUser.Username, id);
                //var Activity = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/DBType/Edit";
                //await _ldapAuhenticationservice.CreateActivityLog(Activity, CurrentUser.Username, CurrentUser.Id);
                return new JsonResult(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Delete")]
        [AppAuthorize(AppPermissions.Master_Infra_InfraDBType_Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                await _IDBtypeservice.DeleteByKeysAsync(id);
                //var Activity = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/DBType/Delete";
                //await _ldapAuhenticationservice.CreateActivityLog(Activity, CurrentUser.Username, CurrentUser.Id);
                return Ok("SuccessFully Deleted");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Search")]
        [AppAuthorize(AppPermissions.Master_Infra_InfraDBType_List)]
        public async Task<IActionResult> GetResult(string dbTypeName = null, string isActive = null)
        {
            if (ModelState.IsValid)
            {
                var dbTypes = await _IDBtypeservice.Search(dbTypeName, isActive);
                //var Activity = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/DBType/Search";
                //await _ldapAuhenticationservice.CreateActivityLog(Activity, CurrentUser.Username, CurrentUser.Id);
                return new JsonResult(dbTypes);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Send")]
        public async Task<IActionResult> SendEmailAsync()
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sushil Buddh", "sushilbuddh@gmail.com"));
                message.To.Add(new MailboxAddress("Sushil Buddha", "buddhasushil5421@gmail.com"));
                message.Subject = "ARMS";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<a href='http://localhost:4200/master/applicationportfoliomaker'>Click here to proceed</a>";

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    await client.AuthenticateAsync("sushilbuddh@gmail.com", "ekfp bgxs vprj fpep");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
