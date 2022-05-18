namespace SimpleValidatorBuilder.Parser;

public sealed class LengthIsExactly<TError> : IParser<string, TError>
{
	private readonly Func<string?, TError> _errorFactory;

	public int ExactLength { get; }

	internal LengthIsExactly(int exactLength, Func<string?, TError> errorFactory)
	{
		ExactLength = exactLength;
		_errorFactory = errorFactory;
	}

	public void Parse(ParserResult<string?, TError> result)
	{
		if (result.Value!.Length != ExactLength)
			result.SetError(_errorFactory(result.Value));
	}
}
