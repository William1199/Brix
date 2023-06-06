using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ContractorPostConfiguration : IEntityTypeConfiguration<ContractorPost>
    {

        public void Configure(EntityTypeBuilder<ContractorPost> builder)
        {
            builder.ToTable("ContractorPosts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.LastModifiedAt).HasDefaultValueSql("getutcdate()");

            builder.HasOne(x => x.Contractor).WithMany(x => x.ContractorPosts).HasForeignKey(x => x.ContractorID);
            builder.HasMany(x => x.Quizzes).WithOne(x => x.ContractorPost).HasForeignKey(x => x.PostID);
        }
    }
}
