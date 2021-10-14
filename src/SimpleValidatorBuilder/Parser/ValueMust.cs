using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class ValueMust<TValue, TError> : IParser<TValue, TError>
{
    public Func<TValue, bool> ValuePredicate { get; }
    public Func<TError> ErrorFactory { get; }

    internal ValueMust(Func<TValue, bool> valuePredicate, Func<TError> errorFactory)
    {
        ValuePredicate = valuePredicate;
        ErrorFactory = errorFactory;
    }

    public Result<TValue, TError> Parse(in TValue value)
        => !ValuePredicate.Invoke(value) ? Result.Failure<TValue, TError>(ErrorFactory.Invoke()) : Result.Success<TValue, TError>(value);
}
