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
        => value.CompareTo(MinValue) < 0 ? Result.Failure<TValue, TError>(ErrorFactory.Invoke()) : Result.Success<TValue, TError>(value);
}
