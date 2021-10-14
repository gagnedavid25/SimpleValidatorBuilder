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
        => RunValidation(value, ExactLength, ErrorFactory.Invoke());

    public static Result<string, TError> RunValidation(in string value, in int exactLength, in TError error)
        => value.Length != exactLength ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);
}
