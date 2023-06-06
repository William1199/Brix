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
    public class SaveConfiguration : IEntityTypeConfiguration<Save>
    {

        public void Configure(EntityTypeBuilder<Save> builder)
        {
            builder.ToTable("Saves");
            builder.HasKey(x => new {x.Id,x.UserId});
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.HasOne(x => x.ContractorPost).WithMany(x => x.Saves).HasForeignKey(x => x.ContractorPostId);
            builder.HasOne(x => x.User).WithMany(x => x.Saves).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
