namespace SimpleValidatorBuilder.Internal.Parser;

internal sealed class ValueMust<TValue, TError> : IParser<TValue, TError>
{
    private readonly Func<TValue?, TError> _errorFactory;

    public Func<TValue?, bool> ValuePredicate { get; }

    internal ValueMust(Func<TValue?, bool> valuePredicate, Func<TValue?, TError> errorFactory)
    {
        ValuePredicate = valuePredicate;
        _errorFactory = errorFactory;
    }

    public void Parse(ParserResult<TValue?, TError> result)
    {
        if (!ValuePredicate(result.Value))
            result.SetError(_errorFactory(result.Value));
    }
}
