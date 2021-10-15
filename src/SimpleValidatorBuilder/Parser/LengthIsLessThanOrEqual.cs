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
        => value.Length > MaxLength ? Result.Failure<string, TError>(ErrorFactory.Invoke()) : Result.Success<string, TError>(value);
}
