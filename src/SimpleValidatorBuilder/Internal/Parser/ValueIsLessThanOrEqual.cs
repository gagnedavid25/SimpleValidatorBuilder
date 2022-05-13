namespace SimpleValidatorBuilder.Internal.Parser;

internal sealed class ValueIsLessThanOrEqual<TValue, TError> : IParser<TValue, TError>
    where TValue : IComparable<TValue>
{
    private readonly Func<TValue?, TError> _errorFactory;

    public TValue MaxValue { get; }

    internal ValueIsLessThanOrEqual(TValue maxValue, Func<TValue?, TError> errorFactory)
    {
        MaxValue = maxValue;
        _errorFactory = errorFactory;
    }

    public void Parse(ParserResult<TValue?, TError> result)
    {
        if (result.Value!.CompareTo(MaxValue) > 0)
            result.SetError(_errorFactory(result.Value));
    }
}
