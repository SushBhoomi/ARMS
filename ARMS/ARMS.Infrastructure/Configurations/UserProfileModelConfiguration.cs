using ARMS.Core.Entities;
using ARMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARMS.Infrastructure.Configurations
{
    public class UserProfileModelConfiguration : IEntityTypeConfiguration<UserProfileModel>
    {
        public void Configure(EntityTypeBuilder<UserProfileModel> builder)
        {
            builder.HasAlternateKey(x => x.Username);
            builder.HasAlternateKey(x => x.AccountNumber);
            builder.Property(e => e.Username).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.Email).IsRequired().HasColumnType("NVARCHAR2(256)").HasMaxLength(256);
            builder.Property(e => e.AccountNumber).IsRequired().HasColumnType("NVARCHAR2(50)").HasMaxLength(50);
            builder.Property(e => e.Phone).HasColumnType("NVARCHAR2(128)").HasMaxLength(128);
            builder.Property(e => e.Title).HasColumnType("NVARCHAR2(128)").HasMaxLength(128);
            builder.Property(e => e.Department).HasColumnType("NVARCHAR2(128)").HasMaxLength(128);
            builder.Property(e => e.DateFormat).HasColumnType("NVARCHAR2(50)").HasDefaultValue(DateFormat.MM_DD_YYYY_LABEL);
        }
    }
}
