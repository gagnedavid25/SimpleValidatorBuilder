using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimpleValidatorBuilderExamples.Domain.Aggregates.CustomerAggregate;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;
using SimpleValidatorBuilderExamples.Persistence;

namespace SimpleValidatorBuilderBenchmark;
/*
[MemoryDiagnoser]
public class TestReadData
{
    private SimpleValidatorBuilderDbContext _context;

    public const string FirstName = "David";
    public const string LastName = "Gagné";

    [GlobalSetup]
    public void GlobalSetup()
    {
        var contextOptions = new DbContextOptionsBuilder<SimpleValidatorBuilderDbContext>()
            .UseInMemoryDatabase("SimpleValidatorBuilderTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _context = new SimpleValidatorBuilderDbContext(contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var firstName = PersonName.Create(FirstName).Value;
        var lastName = PersonName.Create(LastName).Value;
        var user = User.CreateNew(firstName, lastName);

        var firstNameWithoutValidator = PersonNameWithoutValidator.Create(FirstName).Value;
        var lastNameWithoutValidator = PersonNameWithoutValidator.Create(LastName).Value;
        var userWithoutValidator = UserWithoutValidator.CreateNew(firstNameWithoutValidator, lastNameWithoutValidator);

        _context.Add(user);
        _context.Add(userWithoutValidator);
        _context.SaveChanges();
    }

    [Benchmark]
    public void Get_data_with_validator_builder()
    {
        _context.ChangeTracker.Clear();
        var data = _context.Users.First();
    }

    [Benchmark]
    public void Get_data_without_validator_builder()
    {
        _context.ChangeTracker.Clear();
        var data = _context.UserWithoutValidators.First();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _context.Dispose();
    }
}
*/