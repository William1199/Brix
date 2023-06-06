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
    public class WorkerConstructionTypeConfiguration : IEntityTypeConfiguration<WorkerContructionType>
    {
        public void Configure(EntityTypeBuilder<WorkerContructionType> builder)
        {
            builder.ToTable("WorkerContructionTypes");

            builder.HasKey(x => new { x.ConstructionTypeId, x.BuilderId });
            builder.HasOne(x => x.ConstructionType).WithMany(x => x.WorkerContructionTypes).HasForeignKey(x => x.ConstructionTypeId);
            builder.HasOne(x => x.Builder).WithMany(x => x.WorkerContructionTypes).HasForeignKey(x => x.BuilderId);
        }
    }
}
