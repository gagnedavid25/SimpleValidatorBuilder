using CSharpFunctionalExtensions;
using SimpleValidatorBuilder;

namespace SimpleValidatorBuilderExamples.Domain.ValueObjects;

public class Email : SimpleValueObject<string>
{
    const int MaxLength = 320;

    private Email(string value)
        : base(value)
    {
    }

    public static Result<Email, Error> Create(string input)
    {
        var result = Validator.Validate(input);
        return result.Bind<string, Email, Error>(value => new Email(value));
    }

    public readonly static Validator<string, Error> Validator =
        Validate.That<string, Error>()
            .WithTransformation(value => (value ?? "").Trim())
            .StringIsNotNullOrEmpty(() => Errors.ValueIsRequired(nameof(Email)))
            .LengthIsLessThanOrEqual(MaxLength, () => Errors.LengthIsInvalid(nameof(Email), $"Length cannot exeeds {MaxLength} characters"))
            .StringIsEmail(() => Errors.ValueIsInvalid(nameof(Email), "The value is not a valid email"));
}
