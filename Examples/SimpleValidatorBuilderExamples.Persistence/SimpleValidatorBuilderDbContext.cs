using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilderExamples.Domain.Aggregates.CustomerAggregate;

namespace SimpleValidatorBuilderExamples.Persistence
{
    public class SimpleValidatorBuilderDbContext : AppDbContext
    {
        public SimpleValidatorBuilderDbContext(DbContextOptions<SimpleValidatorBuilderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Users { get; set; }
        //public DbSet<UserWithoutValidator> UserWithoutValidators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
