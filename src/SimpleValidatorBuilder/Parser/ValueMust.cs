using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class ValueMust<TValue, TError> : IParser<TValue, TError>
{
    private readonly Func<TError> _errorFactory;

    public Func<TValue, bool> ValuePredicate { get; }

    internal ValueMust(Func<TValue, bool> valuePredicate, Func<TError> errorFactory)
    {
        ValuePredicate = valuePredicate;
        _errorFactory = errorFactory;
    }

    public Result<TValue, TError> Parse(TValue value)
        => !ValuePredicate.Invoke(value) ? Result.Failure<TValue, TError>(_errorFactory.Invoke()) : Result.Success<TValue, TError>(value);
}
