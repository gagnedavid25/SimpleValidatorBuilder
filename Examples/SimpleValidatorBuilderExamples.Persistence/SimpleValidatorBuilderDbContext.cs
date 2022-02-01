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

        DbSet<RegistredUser> RegistredUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
