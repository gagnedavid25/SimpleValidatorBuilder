using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class LengthIsLessThanOrEqual<TError> : IParser<string, TError>
{
    public int MaxLength { get; }
    public Func<TError> ErrorFactory { get; }

    internal LengthIsLessThanOrEqual(int maxLength, Func<TError> errorFactory)
    {
        MaxLength = maxLength;
        ErrorFactory = errorFactory;
    }

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, MaxLength, ErrorFactory.Invoke());

    public static Result<string, TError> RunValidation(in string value, in int maxLength, in TError error)
        => value.Length > maxLength ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);
}
