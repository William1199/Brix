using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quiz");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
              .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Questions).WithOne(x => x.Quiz).HasForeignKey(x => x.QuizId);
            builder.HasMany(x => x.AppliedPosts).WithOne(x => x.Quiz).HasForeignKey(x => x.QuizId);
        }

    }
}
