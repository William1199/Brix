using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class TypeConfiguration : IEntityTypeConfiguration<Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Entities.Type> builder)
        {
            builder.ToTable("Types");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Skill).WithOne(x => x.Type).HasForeignKey(x => x.TypeId).IsRequired(false);
            builder.HasMany(x => x.Quiz).WithOne(x => x.Types).HasForeignKey(x => x.TypeID);

        }
    }
}
