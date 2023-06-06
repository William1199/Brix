using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("BillDetail");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.HasOne(x => x.Bills).WithMany(x => x.BillDetails).HasForeignKey(x => x.BillID);
            builder.HasOne(x => x.Products).WithMany(x => x.BillDetails).HasForeignKey(x => x.ProductID);

        
        }

    }
}
