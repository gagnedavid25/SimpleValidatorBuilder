using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder;

public class Validator<TValue, TError>
{
    public IReadOnlyList<IParser<TValue, TError>> Parsers { get; }

    protected internal Validator(ValidatorBuilder<TValue, TError> validatorBuilder) 
        => Parsers = validatorBuilder.Parsers;

    public Result<TValue, TError> Validate(in TValue value, Func<TError, TError>? modificationsToErrorIfFailure = null)
    {
        Result<TValue, TError> result = Result.Success<TValue, TError>(value);

        for (int i = 0; i < Parsers.Count; i++)
        {
            result = Parsers[i].Parse(result.Value);

            if (result.IsFailure)
                return modificationsToErrorIfFailure is null ? result : modificationsToErrorIfFailure.Invoke(result.Error);
        }

        return result;
    }

    public static implicit operator Validator<TValue, TError>(ValidatorBuilder<TValue, TError> validatorBuilder)
        => new Validator<TValue, TError>(validatorBuilder);
}
