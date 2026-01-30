using ARMS.Application.Common;
using ARMS.Application.Entities.LoginMaster.Services.Interfaces;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.WebAPI.Controllers;
using ARMS.WebAPI.Models;
using ARMS.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using ControllerBase = ARMS.WebAPI.Controllers.ControllerBase;

namespace ARMS.WebAPI.Filters
{
    public class ActivityLogFilter : IAsyncActionFilter
    {
        private readonly ILogger<ActivityLogFilter> _logger;
        private readonly ErrorDBContext _dbcontext;
        private readonly ILoginService _loginService;
        public ActivityLogFilter(ILogger<ActivityLogFilter> logger, ErrorDBContext dbcontext, ILoginService loginservice)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _loginService = loginservice;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var request = context.HttpContext.Request;
            var accessToken = request.Cookies["access"];
            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();
            var parameters = context.ActionArguments;
            var controller = context.Controller as ControllerBase;
            var ipaddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (ipaddress == "::1")
            {
                ipaddress = "127.0.0.1";
            }

            _logger.LogInformation($"Executing action {actionName} in controller {controllerName}");

            if (controller != null && controller is ICurrentUserAccessor currentUserAccessor)
            {
                var currentUser = currentUserAccessor.CurrentUser;

                if (currentUser.Id == 0) // User not authenticated
                {
                    // Extract username from login request
                    string username = null;
                    if (actionName == "Login" && parameters.TryGetValue("model", out var model))
                    {
                        if (model is AdUser adUser)
                        {
                            username = adUser.Username;
                            var user = await _loginService.GetAsync(adUser.Username, adUser.Password);
                            if (user != null)
                            {
                                currentUser.Id = Convert.ToInt32(user.Id); // Assuming user has an Id property
                            }
                        }
                    }

                    var activity = _dbcontext.Activity.FirstOrDefault(a => a.Activity == $"{controllerName}/{actionName}");
                    if (activity == null)
                    {
                        await next();
                        return;
                    }
                    var activityLog = new ActivityLogModel
                    {
                        UserId = currentUser.Id, // Assuming this is a nullable type, so it will be 0 for unauthenticated users
                        UserName = username, // Assign extracted username here
                        ActivityId = activity.Id,
                        Timestamp = DateTime.UtcNow.ToLocalTime(),
                        IpAddress = ipaddress ?? "N/A"
                    };

                    try
                    {
                        await _dbcontext.ActivityLog.AddAsync(activityLog);
                        await _dbcontext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while logging activity.");
                    }
                }
                else
                {
                    var activity =_dbcontext.Activity.FirstOrDefault(a => a.Activity == $"{controllerName}/{actionName}");
                    if (activity == null)
                    {
                        await next();
                        return;
                    }
                    var activityLog = new ActivityLogModel
                    {
                        UserId = currentUser.Id,
                        UserName = currentUser.Username,
                        ActivityId = activity.Id,
                        Timestamp = DateTime.UtcNow.ToLocalTime(),
                        IpAddress = ipaddress ?? "N/A"
                    };

                    try
                    {
                        await _dbcontext.ActivityLog.AddAsync(activityLog);
                        await _dbcontext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while logging activity.");
                    }
                }
            }
        }

    }
    
}
