using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleValidatorBuilderExamples.Domain.Aggregates.SettingEntity;

namespace SimpleValidatorBuilderExamples.Persistence.Configuration.SettingEntity
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");

            builder.Property(e => e.Code)
                .HasColumnName(nameof(UserType.Code));

            builder.HasOne<Domain.Aggregates.UserEntity.UserType>()
                .WithOne()
                .HasForeignKey<Domain.Aggregates.UserEntity.UserType>(e => e.Id);
        }
    }
}
