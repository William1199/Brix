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
    public class PostInviteConfiguration : IEntityTypeConfiguration<PostInvite>
    {
        public void Configure(EntityTypeBuilder<PostInvite> builder)
        {
            builder.ToTable("PostInvite");

            builder.Property(x => x.Id)
                             .ValueGeneratedOnAdd();

            builder.Property(x => x.BuilderId).HasColumnName("WorkerID");


            builder.HasOne(x => x.Builder).WithMany(x => x.PostInvites).HasForeignKey(x => x.BuilderId);
            builder.HasOne(x => x.ContractorPost).WithMany(x => x.PostInvites).HasForeignKey(x => x.ContractorPostId);
            builder.HasOne(x => x.Contractor).WithMany(x => x.PostInvites).HasForeignKey(x => x.ContractorId);
        }

    }
}
