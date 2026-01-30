using ARMS.Application.Interfaces.Repository;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Data;
using ARMS.Core.Entities;
using ARMS.Core.Entities.QueryModels;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories;
using ARMS.Infrastructure.UnitOfWork;
using ARMS.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace ARMS.Infrastructure
{
    public static class ServiceRegistration
    {
        
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseOracle(configuration.GetConnectionString("DefaultConnection"),
            //       b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            //var encryptedConnectionString = configuration["ConnectionStrings:DefaultConnection1"];
            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var connectionStringManager = new ConnectionStringManager(configuration);
            //string con = connectionStringManager.EncryptConnectionString();
            //string connectionString = connectionStringManager.DecryptConnectionString("Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.2.16)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SID = YBLARMS))); User Id=YBL_ARMS; Password=YBLARMS#123;");


            services.AddDbContext<ApplicationDbContext>(options =>
     options.UseOracle(connectionString, b => b
         .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
         // This is the critical line for Oracle 11g
         .UseOracleSQLCompatibility("11")
     ));
            services.AddDbContext<ErrorDBContext>(options =>
                options.UseOracle(connectionString,
                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
         // This is the critical line for Oracle 11g
         .UseOracleSQLCompatibility("11")));



            #region Repositories
            services.AddScoped<IUnitOfWorkProvider, UnitOfWorkProvider>();
            services.AddScoped<IRepository<UserProfileModel>, UserProfileRepository>();
            services.AddScoped<IRepository<UserRequestModel>, UserRequestRepository>();
            services.AddScoped<IRepository<RoleModel>, RoleRepository>();
            services.AddScoped<IRepository<PermissionModel>, PermissionRepository>();
            services.AddScoped<IRepository<LoginMasterModel>, LoginRepository>();
            services.AddScoped<IRepository<DBTypeMasterModel>, DBTypeRepository>();
            services.AddScoped<IRepository<ExceptionLogModel>, ExceptionLogRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IDBTypeRepository, DBTypeRepository>();


            // To execute statements to database via SP, query,...
            services.AddScoped<IDataQuery, EfDataQuery>();

            #endregion Repositories
        }


    }

}
