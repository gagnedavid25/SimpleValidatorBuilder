using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class ValueIsGreaterThanOrEqual<TValue, TError> : IParser<TValue, TError>
    where TValue : IComparable<TValue>
{
    public TValue MinValue { get; }
    public Func<TError> ErrorFactory { get; }

    internal ValueIsGreaterThanOrEqual(TValue minValue, Func<TError> errorFactory)
    {
        MinValue = minValue;
        ErrorFactory = errorFactory;
    }

    public Result<TValue, TError> Parse(in TValue value)
        => RunValidation(value, MinValue, ErrorFactory.Invoke());

    public static Result<TValue, TError> RunValidation(in TValue value, in TValue minValue, in TError error)
    {
        if (value.CompareTo(minValue) < 0)
            return Result.Failure<TValue, TError>(error);

        return Result.Success<TValue, TError>(value);
    }
}

public class DecimalIsGreaterThanOrEqual<TError> : IParser<decimal, TError>
{
    public decimal MinValue { get; }
    public Func<TError> ErrorFactory { get; }

    internal DecimalIsGreaterThanOrEqual(decimal minValue, Func<TError> errorFactory)
    {
        MinValue = minValue;
        ErrorFactory = errorFactory;
    }

    public Result<decimal, TError> Parse(in decimal value)
        => RunValidation(value, MinValue, ErrorFactory.Invoke());

    public static Result<decimal, TError> RunValidation(in decimal value, in decimal minValue, in TError error)
        => ValueIsGreaterThanOrEqual<decimal, TError>.RunValidation(value, minValue, error);
}
