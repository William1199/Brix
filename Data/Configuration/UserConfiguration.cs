using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BuilderId).HasColumnName("WorkerID");
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.LastModifiedAt).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.Email).IsRequired(false);
            //builder.Property(x=>x.ContractorId)

        }
    }
}
