using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class LengthIsLessThanOrEqual<TError> : IParser<string, TError>
{
    private Func<TError> _errorFactory;

    public int MaxLength { get; }

    internal LengthIsLessThanOrEqual(int maxLength, Func<TError> errorFactory)
    {
        _errorFactory = errorFactory;
        MaxLength = maxLength;
    }

    public Result<string, TError> Parse(in string value)
        => value.Length > MaxLength ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);
}
