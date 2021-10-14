using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class ValueIsLessThanOrEqual<TValue, TError> : IParser<TValue, TError>
    where TValue : IComparable<TValue>
{
    public TValue MaxValue { get; }
    public Func<TError> ErrorFactory { get; }

    internal ValueIsLessThanOrEqual(TValue maxValue, Func<TError> errorFactory)
    {
        MaxValue = maxValue;
        ErrorFactory = errorFactory;
    }

    public Result<TValue, TError> Parse(in TValue value)
        => RunValidation(value, MaxValue, ErrorFactory.Invoke());

    public static Result<TValue, TError> RunValidation(in TValue value, in TValue maxValue, in TError error) 
        => value.CompareTo(maxValue) > 0 ? Result.Failure<TValue, TError>(error) : Result.Success<TValue, TError>(value);
}

public class DecimalIsLessThanOrEqual<TError> : IParser<decimal, TError>
{
    public decimal MaxValue { get; }
    public Func<TError> ErrorFactory { get; }

    internal DecimalIsLessThanOrEqual(decimal maxValue, Func<TError> errorFactory)
    {
        MaxValue = maxValue;
        ErrorFactory = errorFactory;
    }

    public Result<decimal, TError> Parse(in decimal value)
        => RunValidation(value, MaxValue, ErrorFactory.Invoke());

    public static Result<decimal, TError> RunValidation(in decimal value, in decimal maxValue, in TError error)
        => ValueIsLessThanOrEqual<decimal, TError>.RunValidation(value, maxValue, error);
}
