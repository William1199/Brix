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
    public class ProductCategoriesConfiguration : IEntityTypeConfiguration<ProductCategories>
    {
        public void Configure(EntityTypeBuilder<ProductCategories> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => new { x.ProductID, x.CategoriesID });


            builder.HasOne(x => x.Categories).WithMany(x => x.ProductCategories).HasForeignKey(x => x.CategoriesID);
            builder.HasOne(x => x.Products).WithMany(x => x.ProductCategories).HasForeignKey(x => x.ProductID);
        }
    }
}
