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
    internal class ExceptionLogModelConfiguration : IEntityTypeConfiguration<ExceptionLogModel>
    {
        public void Configure(EntityTypeBuilder<ExceptionLogModel> builder)
        {
            
            builder.Property(e => e.LogLevel).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.Message).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.Exception).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
        }
    }
}
