using CSharpFunctionalExtensions;
using SimpleValidatorBuilder.Parser;

namespace SimpleValidatorBuilder;

public class Validator<TValue, TError>
{
    private readonly IParser<TValue, TError>[] _parsers;

    public IEnumerable<IParser<TValue, TError>> Parsers 
        => _parsers.ToArray();

    protected internal Validator(IParser<TValue, TError>[] parsers) 
        => _parsers = parsers;

    public Result<TValue?, TError> Validate(TValue? value)
    {
        var result = new ParserResult<TValue?, TError>();
        result.SetValue(value);

        for (int i = 0; i < _parsers.Length; i++)
        {
            _parsers[i].Parse(result);

            if (result.IsFailure)
                return result.Error!;
        }

        return result.Value;
    }

    public static implicit operator Validator<TValue, TError>(ValidatorBuilder<TValue, TError> validatorBuilder)
        => validatorBuilder.Build();
}
