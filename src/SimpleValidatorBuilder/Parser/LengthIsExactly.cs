using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class LengthIsExactly<TError> : IParser<string, TError>
{
    public int ExactLength { get; }
    public TError Error { get; }

    internal LengthIsExactly(int exactLength, TError error)
    {
        ExactLength = exactLength;
        Error = error;
    }

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, ExactLength, Error);

    public static Result<string, TError> RunValidation(in string value, in int exactLength, in TError error)
        => value.Length != exactLength ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);
}
