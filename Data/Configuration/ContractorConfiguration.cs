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
    public class ContractorConfiguration: IEntityTypeConfiguration<Contractor>
    {
        public void Configure(EntityTypeBuilder<Contractor> builder)
        {
            builder.ToTable("Contractors");
            builder.HasKey(x =>x.Id);
            builder.Property(x => x.LastModifiedAt).HasDefaultValueSql("getutcdate()");

            builder
                   .HasOne(x => x.User)
                   .WithOne(x => x.Contractor).HasForeignKey<User>(x => x.ContractorId).IsRequired(false);
        }
    }
}
