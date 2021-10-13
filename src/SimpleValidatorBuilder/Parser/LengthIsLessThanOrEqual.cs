using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class LengthIsLessThanOrEqual<TError> : IParser<string, TError>
{
    public int MaxLength { get; }
    public TError Error { get; }

    internal LengthIsLessThanOrEqual(int maxLength, TError error)
    {
        MaxLength = maxLength;
        Error = error;
    }

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, MaxLength, Error);

    public static Result<string, TError> RunValidation(in string value, in int maxLength, in TError error)
        => value.Length > maxLength ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);
}
