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
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .ValueGeneratedOnAdd();
            builder.Property(x => x.BuilderID).HasColumnName("WorkerID");
            builder
                   .HasOne(x => x.Builder)
                   .WithMany(x => x.Groups).HasForeignKey(x => x.BuilderID).IsRequired(false);

          
        }
    }
}
