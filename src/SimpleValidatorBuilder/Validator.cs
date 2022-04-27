using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder;

public class Validator<TValue, TError>
{
    public IParser<TValue, TError>[] Parsers { get; }

    protected internal Validator(ValidatorBuilder<TValue, TError> validatorBuilder) 
        => Parsers = validatorBuilder.Parsers.ToArray();

    public Result<TValue, TError> Validate(TValue value, Func<TError, TError>? modificationsToErrorIfFailure = null)
    {
        var result = Result.Success<TValue, TError>(value);

        for (int i = 0; i < Parsers.Length; i++)
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
