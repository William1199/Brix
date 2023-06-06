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
    public class ContractorPostTypeConfiguration : IEntityTypeConfiguration<ContractorPostType>
    {

        public void Configure(EntityTypeBuilder<ContractorPostType> builder)
        {
            builder.ToTable("ContractorPostType");
            builder.HasKey(x => new { x.TypeID, x.ContractorPostID });


            builder.HasOne(x => x.ContractorPost).WithMany(x => x.ContractorPostTypes).HasForeignKey(x => x.ContractorPostID);
            builder.HasOne(x => x.Type).WithMany(x => x.ContractorPostTypes).HasForeignKey(x => x.TypeID);
        }
    }
}