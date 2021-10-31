using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class LengthIsGreaterThanOrEqual<TError> : IParser<string, TError>
{
    private readonly Func<TError> _errorFactory;

    public int MinLength { get; }

    internal LengthIsGreaterThanOrEqual(int minLength, Func<TError> errorFactory)
    {
        MinLength = minLength;
        _errorFactory = errorFactory;
    }

    public Result<string, TError> Parse(in string value) 
        => value.Length < MinLength ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);
}
