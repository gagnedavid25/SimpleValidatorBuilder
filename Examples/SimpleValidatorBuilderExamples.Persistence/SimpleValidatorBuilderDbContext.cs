using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

namespace SimpleValidatorBuilderExamples.Persistence
{
    public class SimpleValidatorBuilderDbContext : AppDbContext
    {
        public SimpleValidatorBuilderDbContext(DbContextOptions<SimpleValidatorBuilderDbContext> options)
            : base(options)
        {
        }

        public DbSet<RegistredUser> RegistredUsers { get; set; }
        public DbSet<Domain.Aggregates.SettingEntity.UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SimpleValidatorBuilderDbContext).Assembly);
        }
    }
}
