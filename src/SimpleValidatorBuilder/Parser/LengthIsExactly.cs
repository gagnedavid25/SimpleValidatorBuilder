using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class LengthIsExactly<TError> : IParser<string, TError>
{
    public int ExactLength { get; }
    public Func<TError> ErrorFactory { get; }

    internal LengthIsExactly(int exactLength, Func<TError> errorFactory)
    {
        ExactLength = exactLength;
        ErrorFactory = errorFactory;
    }

    public Result<string, TError> Parse(in string value)
        => value.Length != ExactLength ? Result.Failure<string, TError>(ErrorFactory.Invoke()) : Result.Success<string, TError>(value);
}
