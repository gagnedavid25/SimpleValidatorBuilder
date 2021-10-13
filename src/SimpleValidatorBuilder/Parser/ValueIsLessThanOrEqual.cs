using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class ValueIsLessThanOrEqual<TValue, TError> : IParser<TValue, TError>
    where TValue : IComparable<TValue>
{
    public TValue MaxValue { get; }
    public TError Error { get; }

    internal ValueIsLessThanOrEqual(TValue maxValue, TError error)
    {
        MaxValue = maxValue;
        Error = error;
    }

    public Result<TValue, TError> Parse(in TValue value)
        => RunValidation(value, MaxValue, Error);

    public static Result<TValue, TError> RunValidation(in TValue value, in TValue maxValue, in TError error)
    {
        if (value.CompareTo(maxValue) > 0)
            return Result.Failure<TValue, TError>(error);

        return Result.Success<TValue, TError>(value);
    }
}

public class DecimalIsLessThanOrEqual<TError> : IParser<decimal, TError>
{
    public decimal MaxValue { get; }
    public TError Error { get; }

    internal DecimalIsLessThanOrEqual(decimal maxValue, TError error)
    {
        MaxValue = maxValue;
        Error = error;
    }

    public Result<decimal, TError> Parse(in decimal value)
        => RunValidation(value, MaxValue, Error);

    public static Result<decimal, TError> RunValidation(in decimal value, in decimal maxValue, in TError error)
        => ValueIsLessThanOrEqual<decimal, TError>.RunValidation(value, maxValue, error);
}
