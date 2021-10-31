using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class LengthIsExactly<TError> : IParser<string, TError>
{
    private readonly Func<TError> _errorFactory;

    public int ExactLength { get; }

    internal LengthIsExactly(int exactLength, Func<TError> errorFactory)
    {
        _errorFactory = errorFactory;
        ExactLength = exactLength;
    }

    public Result<string, TError> Parse(in string value)
        => value.Length != ExactLength ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);
}
