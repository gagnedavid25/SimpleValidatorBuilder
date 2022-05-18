namespace SimpleValidatorBuilder.Parser;

public sealed class LengthIsLessThanOrEqual<TError> : IParser<string, TError>
{
	private readonly Func<string?, TError> _errorFactory;

	public int MaxLength { get; }

	internal LengthIsLessThanOrEqual(int maxLength, Func<string?, TError> errorFactory)
	{
		MaxLength = maxLength;
		_errorFactory = errorFactory;
	}

	public void Parse(ParserResult<string?, TError> result)
	{
		if (result.Value!.Length > MaxLength)
			result.SetError(_errorFactory(result.Value));
	}
}
