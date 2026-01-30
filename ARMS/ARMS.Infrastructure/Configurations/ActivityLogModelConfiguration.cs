using ARMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Infrastructure.Configurations
{
    internal class ActivityLogModelConfiguration : IEntityTypeConfiguration<ActivityLogModel>
    {
        public void Configure(EntityTypeBuilder<ActivityLogModel> builder)
        {
            builder.HasOne(pt => pt.Activity)
                .WithMany(p => p.Activities)
                .HasForeignKey(pt => pt.ActivityId);

            builder.Property(e => e.IpAddress).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
        }
    }
}
