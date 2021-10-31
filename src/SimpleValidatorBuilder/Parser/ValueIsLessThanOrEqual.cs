using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class ValueIsLessThanOrEqual<TValue, TError> : IParser<TValue, TError>
    where TValue : IComparable<TValue>
{
    private readonly Func<TError> _errorFactory;

    public TValue MaxValue { get; }

    internal ValueIsLessThanOrEqual(TValue maxValue, Func<TError> errorFactory)
    {
        _errorFactory = errorFactory;
        MaxValue = maxValue;
    }

    public Result<TValue, TError> Parse(in TValue value)
        => value.CompareTo(MaxValue) > 0 ? Result.Failure<TValue, TError>(_errorFactory.Invoke()) : Result.Success<TValue, TError>(value);
}
