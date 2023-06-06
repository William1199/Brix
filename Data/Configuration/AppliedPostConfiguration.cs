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
    public class AppliedPostConfiguration : IEntityTypeConfiguration<AppliedPost>
    {
        public void Configure(EntityTypeBuilder<AppliedPost> builder)
        {
            builder.ToTable("AppliedPost");

            builder.HasKey(x => new { x.PostID, x.BuilderID });
            builder.Property(x => x.BuilderID).HasColumnName("WorkerID");



            builder.HasOne(x => x.Builder).WithMany(x => x.AppliedPosts).HasForeignKey(x => x.BuilderID);
            builder.HasOne(x => x.ContractorPosts).WithMany(x => x.AppliedPosts).HasForeignKey(x => x.PostID);
            builder.HasOne(x => x.Group).WithMany(x => x.AppliedPosts).HasForeignKey(x => x.GroupID).IsRequired(false);
        }

    }
}
