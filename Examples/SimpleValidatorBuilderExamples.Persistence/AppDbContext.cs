using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilderExamples.Domain;
using SimpleValidatorBuilderExamples.Domain.Aggregates;
using SimpleValidatorBuilderExamples.Persistence.Extensions;

namespace SimpleValidatorBuilderExamples.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    DbSet<RegistredUser> RegistredUsers { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSimpleValueObjects(typeof(Error).Assembly);
    }
}
