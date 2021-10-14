using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class StringIsNotNullOrEmpty<TError> : IParser<string, TError>
{
    public Func<TError> ErrorFactory { get; }

    internal StringIsNotNullOrEmpty(Func<TError> errorFactory)
        => ErrorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, ErrorFactory.Invoke());

    public static Result<string, TError> RunValidation(in string value, in TError error)
        => string.IsNullOrEmpty(value) ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);
}
