using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class StringIsNotNullOrEmpty<TError> : IParser<string, TError>
{
    public Func<TError> ErrorFactory { get; }

    internal StringIsNotNullOrEmpty(Func<TError> errorFactory)
        => ErrorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => string.IsNullOrEmpty(value) ? Result.Failure<string, TError>(ErrorFactory.Invoke()) : Result.Success<string, TError>(value);
}
