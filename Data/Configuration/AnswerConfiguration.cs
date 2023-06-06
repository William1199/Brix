using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.HasMany(x => x.UserAnswers).WithOne(x => x.Answer).HasForeignKey(x => x.AnswerID);


        }
    }
}
