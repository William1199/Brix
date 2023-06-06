using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Products).WithMany(x => x.ProductTypes).HasForeignKey(x => x.ProductID);


            builder.HasOne(x => x.Color).WithMany(x => x.ProductTypes).HasForeignKey(x => x.ColorId);
            builder.HasOne(x => x.Size).WithMany(x => x.ProductTypes).HasForeignKey(x => x.SizeID);
            builder.HasOne(x => x.Other).WithMany(x => x.ProductTypes).HasForeignKey(x => x.OtherID);
            builder
               .HasMany(x => x.BillDetails)
               .WithOne(x => x.ProductTypes).HasForeignKey(x => x.ProductTypeId).IsRequired(false);
        }

    }
}
