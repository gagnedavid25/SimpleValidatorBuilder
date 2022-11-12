using CSharpFunctionalExtensions;
using SimpleValidatorBuilder;

namespace SimpleValidatorBuilderExamples.Domain.ValueObjects;

public abstract class PostCode : SimpleValueObject<string>
{
    protected PostCode(string postCode)
        : base(postCode)
    {
    }

    public static readonly ValidatorBuilder<string, Error> Validator =
        Validate.That<string, Error>()
            .WithTransformation(value => (value ?? "").Trim())
            .StringIsNotNullOrEmpty(() => Errors.ValueIsRequired(nameof(PostCode)));
}

public class CanadianPostCode : PostCode
{
    public const int Length = 6;

    private CanadianPostCode(string postCode)
        : base(postCode)
    {
    }

    public static CanadianPostCode Create(string canadianPostCode)
    {
        var result = Validator.Validate(canadianPostCode);
        return new CanadianPostCode(result.Value);
    }

    public new static readonly Validator<string, Error> Validator =
        PostCode.Validator
            .LengthIsExactly(Length, () => Errors.LengthIsInvalid(nameof(CanadianPostCode)));
}