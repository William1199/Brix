using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class VerifyConfiguration : IEntityTypeConfiguration<Verify>
    {
        public void Configure(EntityTypeBuilder<Verify> builder)
        {
            builder.ToTable("IdentitficationCards");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.Verifies).HasForeignKey(x => x.UserID);
        }
    }
}
