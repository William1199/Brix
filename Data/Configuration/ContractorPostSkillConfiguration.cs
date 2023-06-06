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
    internal class ContractorPostSkillConfiguration : IEntityTypeConfiguration<ContractorPostSkill>
    {

        public void Configure(EntityTypeBuilder<ContractorPostSkill> builder)
        {
            builder.ToTable("ContractorPostSkills");
            builder.HasKey(x => new { x.SkillID, x.ContractorPostID });


            builder.HasOne(x => x.ContractorPost).WithMany(x => x.PostSkills).HasForeignKey(x => x.ContractorPostID);
            builder.HasOne(x => x.Skills).WithMany(x => x.ContractorPostSkills).HasForeignKey(x => x.SkillID);
        }
    }
}
