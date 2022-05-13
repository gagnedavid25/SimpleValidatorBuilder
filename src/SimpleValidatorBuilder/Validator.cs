using CSharpFunctionalExtensions;
using SimpleValidatorBuilder.Internal;

namespace SimpleValidatorBuilder;

public class Validator<TValue, TError>
{
    private IParser<TValue, TError>[] _parsers;

    protected internal Validator(ValidatorBuilder<TValue, TError> validatorBuilder) 
        => _parsers = validatorBuilder.Parsers.ToArray();

    public Result<TValue?, TError> Validate(TValue? value)
    {
        var result = new ParserResult<TValue?, TError>(value);

        for (int i = 0; i < _parsers.Length; i++)
        {
            _parsers[i].Parse(result);

            if (result.IsFailure)
                return result.Error!;
        }

        return result.Value;
    }

    public static implicit operator Validator<TValue, TError>(ValidatorBuilder<TValue, TError> validatorBuilder)
        => new Validator<TValue, TError>(validatorBuilder);
}
