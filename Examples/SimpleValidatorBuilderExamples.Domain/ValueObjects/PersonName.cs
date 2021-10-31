using SimpleValidatorBuilder;
using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilderExamples.Domain.ValueObjects;

public class PersonName : SimpleValueObject<string>
{
    public const int MaxLength = 60;
    
    private PersonName(string personName)
        : base(personName)
    {
    }

    public static Result<PersonName, Error> Create(string input)
    {
        var result = Validator.Validate(input);
        return result.Bind<string, PersonName, Error>(value => new PersonName(value));
    }

    public readonly static Validator<string, Error> Validator =
        Validate.That<string, Error>()
            .WithTransformation(value => (value ?? "").Trim())
            .StringIsNotNullOrEmpty(() => Errors.ValueIsRequired(nameof(PersonName)))
            .LengthIsLessThanOrEqual(MaxLength, () => Errors.LengthIsInvalid(nameof(PersonName), $"Length cannot exeeds {MaxLength} characters"));
}
