using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class StringIsNotNullOrEmpty<TError> : IParser<string, TError>
{
    private Func<TError> _errorFactory;

    internal StringIsNotNullOrEmpty(Func<TError> errorFactory)
        => _errorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => string.IsNullOrEmpty(value) ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);
}
