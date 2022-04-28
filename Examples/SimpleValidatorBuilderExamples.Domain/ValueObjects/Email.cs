﻿using CSharpFunctionalExtensions;
using SimpleValidatorBuilder;

namespace SimpleValidatorBuilderExamples.Domain.ValueObjects;

public class Email : SimpleValueObject<string>
{
    const int MaxLength = 320;

    private Email(string value)
        : base(value)
    {
    }

    public static Email Create(string input)
    {
        var result = Validator.Validate(input);
        return new Email(result.Value);
    }

    public static readonly Validator<string, Error> Validator =
        Validate.That<string, Error>()
            .WithTransformation(value => (value ?? "").Trim())
            .StringIsNotNullOrEmpty(() => Errors.ValueIsRequired(nameof(Email)))
            .LengthIsLessThanOrEqual(MaxLength, () => Errors.LengthIsInvalid(nameof(Email), $"Length cannot exeeds {MaxLength} characters"))
            .StringIsEmail(() => Errors.ValueIsInvalid(nameof(Email), "The value is not a valid email"));
}
