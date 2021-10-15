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
        => value.CompareTo(MaxValue) > 0 ? Result.Failure<TValue, TError>(ErrorFactory.Invoke()) : Result.Success<TValue, TError>(value);
}
