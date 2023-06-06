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
    public class ConstructionTypeConfiguration : IEntityTypeConfiguration<ConstructionType>
    {
        public void Configure(EntityTypeBuilder<ConstructionType> builder)
        {
            builder.ToTable("ConstructionTypes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


        }

    }
}
