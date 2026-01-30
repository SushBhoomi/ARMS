using ARMS.Core.Entities;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARMS.Infrastructure.Configurations
{
    public class RoleModelConfiguration : IEntityTypeConfiguration<RoleModel>
    {
        public void Configure(EntityTypeBuilder<RoleModel> builder)
        {
            var createdDate = new DateTime(2024, 01, 01);
            builder.Property(e => e.Name).IsRequired();
            builder.HasData
            (
                new RoleModel() { Id = 1, Name = "System Admin", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate, Status = RoleStatus.Active,UpdatedBy= AppConstant.SYSTEM_USERNAME },
                new RoleModel() { Id = 2, Name = "CSR", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate, Status = RoleStatus.Active, UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new RoleModel() { Id = 3, Name = "Registered User", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate, Status = RoleStatus.Active, UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new RoleModel() { Id = 4, Name = "Guest", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate, Status = RoleStatus.Active, UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new RoleModel() { Id = 5, Name = "Pending", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate, Status = RoleStatus.Active , UpdatedBy = AppConstant.SYSTEM_USERNAME }
            );
        }
    }
}
