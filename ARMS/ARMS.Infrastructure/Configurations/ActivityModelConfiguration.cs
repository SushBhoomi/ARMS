using ARMS.Core.Entities;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Infrastructure.Configurations
{
    public class ActivityModelConfiguration : IEntityTypeConfiguration<ActivityModel>
    {
        public void Configure(EntityTypeBuilder<ActivityModel> builder)
        {
            var createdDate = new DateTime(2024, 01, 01);
            builder.Property(e => e.Activity).IsRequired();
            builder.HasData
            (
                new ActivityModel() { Id = 1, Activity = "DBType/Get",Message="Getting List of DBType", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate,  UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new ActivityModel() { Id = 2, Activity = "DBType/Insert", Message = "Insert Data in DBType", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate,  UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new ActivityModel() { Id = 3, Activity = "DBType/Edit", Message = "Edit Data in DBType", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate,  UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new ActivityModel() { Id = 4, Activity = "DBType/Delete", Message = "Delete Data in DBType", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate,  UpdatedBy = AppConstant.SYSTEM_USERNAME },
                new ActivityModel() { Id = 5, Activity = "DBType/Search", Message = "Search Data in DBType", CreatedBy = AppConstant.SYSTEM_USERNAME, CreatedDate = createdDate,  UpdatedBy = AppConstant.SYSTEM_USERNAME }
            );
        }
    }
}
