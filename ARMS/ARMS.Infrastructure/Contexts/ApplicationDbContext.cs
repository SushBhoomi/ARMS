using ARMS.Core.Entities;
using ARMS.Core.Entities.QueryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ARMS.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Tables
        public virtual DbSet<UserProfileModel> UserProfiles { get; set; }
        public virtual DbSet<UserRoleModel> UserRoles { get; set; }
        public virtual DbSet<RoleModel> Roles { get; set; }
        public virtual DbSet<PermissionModel> Permissions { get; set; }
        public virtual DbSet<RolePermissionModel> RolePermissions { get; set; }
        public virtual DbSet<LoginMasterModel> LoginMaster { get; set; }
        public virtual DbSet<DBTypeMasterModel> DBTypeMaster { get; set; }
        
        
        


        #endregion

        #region Views

        public virtual DbSet<UserRequestModel> UserRequestsView { get; set; }
        public DbSet<UserListQueryModel> UserListQuery { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRequestModel>(c =>
            {
                c.HasNoKey().ToView("vw_UserRequests");
            });
            modelBuilder.Entity<UserListQueryModel>(c =>
            {
                c.HasNoKey().ToView(null);
            });
            
            // Load Auto-mapper profiles
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
