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

        public DbSet<User> Users { get; set; }
        public DbSet<UserWithoutValidator> UserWithoutValidators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
