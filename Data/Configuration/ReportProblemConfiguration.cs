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
    public class ReportProblemConfiguration : IEntityTypeConfiguration<Report>
    {

        public void Configure(EntityTypeBuilder<Report> report)
        {
            report.ToTable("Reports");
            report.HasKey(x => x.Id);
            report.Property(x => x.Id).ValueGeneratedOnAdd();


            report.HasOne(x => x.ContractorPosts).WithMany(x => x.Reports).HasForeignKey(x => x.ContractorPostId).IsRequired(false);
            report.HasOne(x => x.Products).WithMany(x => x.Reports).HasForeignKey(x => x.ProductId).IsRequired(false);
            report.HasOne(x => x.User).WithMany(x => x.Reports).HasForeignKey(x => x.UserId);

        }
    }
}
