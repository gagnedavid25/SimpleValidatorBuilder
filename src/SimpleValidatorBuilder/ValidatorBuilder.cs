using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder;

public class ValidatorBuilder<TValue, TError>
{
    private readonly List<IParser<TValue, TError>> _parsers;
    public IReadOnlyList<IParser<TValue, TError>> Parsers
        => _parsers.AsReadOnly();

    internal ValidatorBuilder()
        => _parsers = new List<IParser<TValue, TError>>();

    public void AddParser(in IParser<TValue, TError> validation)
        => _parsers.Add(validation);

    public Result<TValue, TError> Validate(in TValue value)
    {
        Result<TValue, TError> result = Result.Success<TValue, TError>(value);

        for (int i = 0; i < _parsers.Count; i++)
        {
            result = _parsers[i].Parse(result.Value);

            if (result.IsFailure)
                return result;
        }

        return result;
    }
}
