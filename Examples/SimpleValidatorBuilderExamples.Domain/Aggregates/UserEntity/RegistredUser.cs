using CSharpFunctionalExtensions;
using Dawn;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

public class RegistredUser : Entity
{
    public PersonName FirstName { get; private set; }
    public PersonName? LastName { get; private set; }
    public UserType UserType { get; private set; }

    private RegistredUser() {}
    
    private RegistredUser(PersonName firstName, PersonName? lastName, UserType userType)
    {
        FirstName = Guard.Argument(firstName, nameof(firstName)).NotNull();
        LastName = lastName;
        UserType = userType;
    }

    public static RegistredUser CreateNew(PersonName firstName, PersonName? lastName, long userTypeId)
        => new RegistredUser(firstName, lastName, new UserType(userTypeId));
}
