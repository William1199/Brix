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
    public class MaterialStoreConfiguration : IEntityTypeConfiguration<MaterialStore>
    {

        public void Configure(EntityTypeBuilder<MaterialStore> builder)
        {
            builder.ToTable("MaterialStores");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.LastModifiedAt).HasDefaultValueSql("getutcdate()");


            builder
                   .HasOne(x => x.User)
                   .WithOne(x => x.MaterialStore).HasForeignKey<User>(x => x.MaterialStoreID).IsRequired(false);

        }
    }
}
