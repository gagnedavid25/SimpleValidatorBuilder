using CSharpFunctionalExtensions;
using SimpleValidatorBuilder;

namespace SimpleValidatorBuilderExamples.Domain.ValueObjects;

public class PersonName : SimpleValueObject<string>
{
    public const int MaxLength = 60;

    private PersonName(string personName)
        : base(personName)
    {
    }

    public static PersonName Create(string input)
    {
        var result = Validator.Validate(input);
        return new PersonName(result.Value);
    }

    public static readonly Validator<string, Error> Validator =
        Validate.That<string, Error>()
            .WithTransformation(value => (value ?? "").Trim())
            .StringIsNotNullOrEmpty(_ => Errors.ValueIsRequired(nameof(PersonName)))
            .LengthIsLessThanOrEqual(MaxLength, _ => Errors.LengthIsInvalid(nameof(PersonName), $"Length cannot exeeds {MaxLength} characters"));
}
