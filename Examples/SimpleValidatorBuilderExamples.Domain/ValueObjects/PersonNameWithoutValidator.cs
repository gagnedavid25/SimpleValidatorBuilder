﻿using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilderExamples.Domain.ValueObjects;

public class PersonNameWithoutValidator : SimpleValueObject<string>
{
    public const int MaxLength = 60;

    private PersonNameWithoutValidator(string value)
        : base(value)
    {
    }

    public static Result<PersonNameWithoutValidator, Error> Create(string input)
    {
        var name = (input ?? "").Trim();

        if (string.IsNullOrEmpty(name))
            return Errors.ValueIsRequired(nameof(PersonNameWithoutValidator));

        if (name.Length > MaxLength)
            return Errors.LengthIsInvalid(nameof(PersonNameWithoutValidator), $"Length cannot exeeds {MaxLength} characters");

        return new PersonNameWithoutValidator(name);
    }
}
