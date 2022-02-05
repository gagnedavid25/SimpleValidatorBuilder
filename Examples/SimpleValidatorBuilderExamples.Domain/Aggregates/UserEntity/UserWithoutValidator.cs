using CSharpFunctionalExtensions;
using Dawn;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

public class UserWithoutValidator : Entity
{
    public PersonNameWithoutValidator FirstName { get; private set; }
    public PersonNameWithoutValidator? LastName { get; private set; }

    private UserWithoutValidator(PersonNameWithoutValidator firstName, PersonNameWithoutValidator? lastName)
    {
        FirstName = Guard.Argument(firstName, nameof(firstName)).NotNull();
        LastName = lastName;
    }

    public static UserWithoutValidator CreateNew(PersonNameWithoutValidator firstName, PersonNameWithoutValidator? lastName)
        => new UserWithoutValidator(firstName, lastName);
}
