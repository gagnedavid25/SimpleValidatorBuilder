using CSharpFunctionalExtensions;
using Dawn;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

public class User : Entity
{
    public PersonName FirstName { get; private set; }
    public PersonName? LastName { get; private set; }

    private User(PersonName firstName, PersonName? lastName)
    {
        FirstName = Guard.Argument(firstName, nameof(firstName)).NotNull();
        LastName = lastName;
    }

    public static User CreateNew(PersonName firstName, PersonName? lastName)
        => new User(firstName, lastName);
}
