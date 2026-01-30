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
    internal class LoginMasterModelConfiguration : IEntityTypeConfiguration<LoginMasterModel>
    {
        public void Configure(EntityTypeBuilder<LoginMasterModel> builder)
        {
            builder.HasAlternateKey(x => x.Username);
            builder.HasAlternateKey(x => x.AccountNumber);
            builder.Property(e => e.Username).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.Password).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.Email).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.AccountNumber).IsRequired().HasColumnType("NVARCHAR2(50)").HasMaxLength(50);
            builder.Property(e => e.Phone).HasColumnType("NVARCHAR2(128)").HasMaxLength(128);
            builder.Property(e => e.Title).HasColumnType("NVARCHAR2(128)").HasMaxLength(128);
            
            
        }

        
    }
}
