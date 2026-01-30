using ARMS.Application;
using ARMS.Application.Caching;
using ARMS.Application.Common;
using ARMS.Application.Entities.ExceptionLog.Services;
using ARMS.Application.Entities.ExceptionLog.Services.Interface;
using ARMS.Application.Exceptions;
using ARMS.Infrastructure;
using ARMS.WebAPI.Controllers;
using ARMS.WebAPI.Extensions;
using ARMS.WebAPI.Filters;
using ARMS.WebAPI.Services;
using ARMS.WebAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Novell.Directory.Ldap;
using Serilog;
using Serilog.Events;
using static ARMS.Core.Models.AppConfig;



var builder = WebApplication.CreateBuilder(args);
//Log.Logger = new LoggerConfiguration()
//                .MinimumLevel.Information()
//                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//                .WriteTo.File("Logs\\Log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
//                .CreateLogger();

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Error()
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
    .CreateLogger();

// Add services to the container.
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//    .MinimumLevel.Override("System", LogEventLevel.Warning)
//    .WriteTo.File("Logs\\Log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
//    .CreateLogger();


//Log.Logger = new LoggerConfiguration()
//                .Enrich.FromLogContext()
//                .MinimumLevel.Information()
//                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//                .WriteTo.File("Logs\\Log-.txt",
//                    rollingInterval: RollingInterval.Day,
//                    fileSizeLimitBytes: 1000000,
//                    rollOnFileSizeLimit: true,
//                    shared: true)
//                .CreateLogger();

builder.Host.UseSerilog(Log.Logger);
builder.Services.AddSingleton<IdentityInfo>();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(AppAuthorizationFilter));
    options.Filters.Add(typeof(AppExceptionFilter));
    options.Filters.Add(typeof(ActivityLogFilter));
    options.Filters.Add(typeof(AppDataMemoryCacheFilter));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ICurrentUserAccessor, ControllerBase>();
builder.Services.AddScoped<ActivityLogFilter>();
builder.Services.Configure<LdapConfig>(builder.Configuration.GetSection("Ldap"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder => builder.WithOrigins("http://127.0.0.1:5500", "https://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddAuthenticationExtension(builder.Configuration);
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
builder.Services.AddSingleton<AppDbConnections>();
builder.Services.AddSingleton<ILdapConnection>(provider =>
{
    // Configure and return an instance of LdapConnection
    return new LdapConnection();
});
builder.Services.AddScoped<ILdapAuhenticationService, LdapAuthentication>();
builder.Services.AddScoped<IActivitiesService, ActivityService>();





var app = builder.Build();

Log.Information("Application startup middleware registration");




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}
else
{
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
