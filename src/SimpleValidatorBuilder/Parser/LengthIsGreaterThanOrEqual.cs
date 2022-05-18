namespace SimpleValidatorBuilder.Parser;

public sealed class LengthIsGreaterThanOrEqual<TError> : IParser<string, TError>
{
	private readonly Func<string?, TError> _errorFactory;

	public int MinLength { get; }

	internal LengthIsGreaterThanOrEqual(int minLength, Func<string?, TError> errorFactory)
	{
		MinLength = minLength;
		_errorFactory = errorFactory;
	}

	public void Parse(ParserResult<string?, TError> result)
	{
		if (result.Value!.Length < MinLength)
			result.SetError(_errorFactory(result.Value));
	}
}
