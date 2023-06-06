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
    public class PostCommitmentsConfigurations : IEntityTypeConfiguration<PostCommitment>
    {
        public void Configure(EntityTypeBuilder<PostCommitment> builder)
        {
            builder.ToTable("Commitment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

            builder.Property(x => x.BuilderID).HasColumnName("WorkerID");


            builder.HasOne(x => x.Builder).WithMany(x => x.PostCommitments).HasForeignKey(x => x.BuilderID);
            builder.HasOne(x => x.ContractorPosts).WithMany(x => x.PostCommitments).HasForeignKey(x => x.PostID);
            builder.HasOne(x => x.Contractor).WithMany(x => x.PostCommitments).HasForeignKey(x => x.ContractorID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Group).WithMany(x => x.PostCommitments).HasForeignKey(x => x.GroupID).IsRequired(false);
        }

    }
}
