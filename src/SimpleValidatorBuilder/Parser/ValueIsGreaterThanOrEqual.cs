using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class ValueIsGreaterThanOrEqual<TValue, TError> : IParser<TValue, TError>
    where TValue : IComparable<TValue>
{
    private Func<TError> _errorFactory;

    public TValue MinValue { get; }

    internal ValueIsGreaterThanOrEqual(TValue minValue, Func<TError> errorFactory)
    {
        _errorFactory = errorFactory;
        MinValue = minValue;
    }

    public Result<TValue, TError> Parse(in TValue value)
        => value.CompareTo(MinValue) < 0 ? Result.Failure<TValue, TError>(_errorFactory.Invoke()) : Result.Success<TValue, TError>(value);
}
