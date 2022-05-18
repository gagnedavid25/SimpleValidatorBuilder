namespace SimpleValidatorBuilder.Parser;

public sealed class ValueIsGreaterThanOrEqual<TValue, TError> : IParser<TValue, TError>
	where TValue : IComparable<TValue>
{
	private readonly Func<TValue?, TError> _errorFactory;

	public TValue MinValue { get; }

	internal ValueIsGreaterThanOrEqual(TValue minValue, Func<TValue?, TError> errorFactory)
	{
		MinValue = minValue;
		_errorFactory = errorFactory;
	}

	public void Parse(ParserResult<TValue?, TError> result)
	{
		if (result.Value!.CompareTo(MinValue) < 0)
			result.SetError(_errorFactory(result.Value));
	}
}
