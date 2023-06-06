using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class BuilderSkillConfiguration : IEntityTypeConfiguration<BuilderSkill>
    {
        public void Configure(EntityTypeBuilder<BuilderSkill> builder)
        {
            builder.ToTable("WorkerSkills");

            builder.HasKey(x => new { x.BuilderSkillID, x.SkillID });
            builder.Property(x => x.BuilderSkillID).HasColumnName("WorkerSkillID");




            builder.HasOne(x => x.Builder).WithMany(x => x.BuilderSkills).HasForeignKey(x => x.BuilderSkillID);
            builder.HasOne(x => x.Skill).WithMany(x => x.BuilderSkills).HasForeignKey(x => x.SkillID);
        }

    }
}
