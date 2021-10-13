using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class StringIsNotNullOrEmpty<TError> : IParser<string, TError>
{
    public TError Error { get; }

    internal StringIsNotNullOrEmpty(TError error)
        => Error = error;

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, Error);

    public static Result<string, TError> RunValidation(in string value, in TError error)
        => string.IsNullOrEmpty(value) ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);
}
