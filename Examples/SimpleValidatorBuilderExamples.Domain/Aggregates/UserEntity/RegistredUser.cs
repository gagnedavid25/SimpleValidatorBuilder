﻿using CSharpFunctionalExtensions;
using Dawn;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;

public class RegistredUser : Entity
{
    public PersonName FirstName { get; private set; }
    public PersonName? LastName { get; private set; }

    private RegistredUser(PersonName firstName, PersonName? lastName)
    {
        FirstName = Guard.Argument(firstName, nameof(firstName)).NotNull();
        LastName = lastName;
    }

    public static RegistredUser CreateNew(PersonName firstName, PersonName? lastName)
        => new RegistredUser(firstName, lastName);
}