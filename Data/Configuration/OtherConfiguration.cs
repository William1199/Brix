using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Configuration
{
    public class OtherConfiguration : IEntityTypeConfiguration<Other>
    {
        public void Configure(EntityTypeBuilder<Other> builder)
        {
            builder.ToTable("Other");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .ValueGeneratedOnAdd();

        }

    }
}
