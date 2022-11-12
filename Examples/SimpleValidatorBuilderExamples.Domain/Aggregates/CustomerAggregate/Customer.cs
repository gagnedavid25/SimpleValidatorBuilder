using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.CustomerAggregate;

public class Customer : Entity
{
    public PersonName FirstName { get; private set; }
    public PersonName LastName { get; private set; }
    public PostCode PostCode { get; private set; }

    private Customer(PersonName firstName, PersonName lastName, PostCode postCode)
    {
        FirstName = Guard.Against.Null(firstName);
        LastName = Guard.Against.Null(lastName);
        PostCode = Guard.Against.Null(postCode);
    }

    public static Customer CreateNew(PersonName firstName, PersonName lastName, PostCode postCode)
        => new Customer(firstName, lastName, postCode);
}
