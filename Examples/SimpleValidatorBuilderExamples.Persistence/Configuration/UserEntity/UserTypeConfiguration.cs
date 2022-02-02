using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

namespace SimpleValidatorBuilderExamples.Persistence.Configuration.UserEntity
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");

            builder.Property(e => e.Code)
                .HasColumnName(nameof(UserType.Code));

            builder.HasOne<Domain.Aggregates.SettingEntity.UserType>()
                .WithOne()
                .HasForeignKey<Domain.Aggregates.SettingEntity.UserType>(e => e.Id);
        }
    }
}
