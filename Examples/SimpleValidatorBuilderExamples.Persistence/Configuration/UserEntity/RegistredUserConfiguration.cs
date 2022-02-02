using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

namespace SimpleValidatorBuilderExamples.Persistence.Configuration.UserEntity
{
    public class RegistredUserConfiguration : IEntityTypeConfiguration<RegistredUser>
    {
        public void Configure(EntityTypeBuilder<RegistredUser> builder)
        {
            //builder.HasOne(e => e.UserType)
            //    .WithMany()
            //    .HasForeignKey(e => e.UserType);
        }
    }
}
