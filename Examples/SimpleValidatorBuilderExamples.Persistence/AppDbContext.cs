using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilder.Extensions.EntityFrameworkCore6;
using SimpleValidatorBuilderExamples.Domain;
using SimpleValidatorBuilderExamples.Persistence.Extensions;

namespace SimpleValidatorBuilderExamples.Persistence;

public abstract class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        var domainAssembly = typeof(Error).Assembly;

        configurationBuilder
            .ConfigureSimpleValueObjects(domainAssembly)
            .ApplyPropertiesConfigurationsUsingStaticValidator(domainAssembly);
    }
}
