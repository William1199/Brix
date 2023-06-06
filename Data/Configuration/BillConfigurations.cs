using Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.HasOne(x => x.Contractor).WithMany(x => x.Bills).HasForeignKey(x => x.ContractorId);
            builder.HasOne(x => x.MaterialStore).WithMany(x => x.Bills).HasForeignKey(x => x.StoreID);
        }

    }

}
