using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.Mappers;
using ARMS.Application.Entities.DBType.Services;
using ARMS.Application.Entities.DBType.Services.Interfece;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.ExceptionLog.Services;
using ARMS.Application.Entities.ExceptionLog.Services.Interface;
using ARMS.Application.Entities.LoginMaster.Services;
using ARMS.Application.Entities.LoginMaster.Services.Interfaces;
using ARMS.Application.Entities.Permissions.Services;
using ARMS.Application.Entities.Permissions.Services.Interfaces;
using ARMS.Application.Entities.Users.Services;
using ARMS.Application.Entities.Users.Services.Interfaces;
using ARMS.Core.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ARMS.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            #region Services

            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserRequestService, UserRequestService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IDBTypeService, DBTypeService>();
            services.AddScoped<IExceptionLog, ExceptionLogService>();
            services.AddScoped<ExceptionLogService>();

            #endregion Services

           

        }
    }
}
