using ARMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Infrastructure.Configurations
{
    public class DBTypeConfiguration : IEntityTypeConfiguration<DBTypeMasterModel>
    {
        

        public void Configure(EntityTypeBuilder<DBTypeMasterModel> builder)
        {
            builder.HasAlternateKey(x => x.DBTypeName);
            builder.Property(e => e.DBTypeName).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.IsActive).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
        }
    }
}
