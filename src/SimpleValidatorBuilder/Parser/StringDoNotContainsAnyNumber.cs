namespace SimpleValidatorBuilder.Parser;

public sealed class StringDoNotContainsAnyNumber<TError> : IParser<string, TError>
{
	private readonly Func<string?, TError> _errorFactory;

	internal StringDoNotContainsAnyNumber(Func<string?, TError> errorFactory)
		=> _errorFactory = errorFactory;

	public void Parse(ParserResult<string?, TError> result)
	{
		if (ContainsNumber(result.Value))
			result.SetError(_errorFactory(result.Value));
	}

	public static bool ContainsNumber(string? str)
	{
		if (string.IsNullOrEmpty(str))
			return false;

		for (int i = 0; i < str.Length; i++)
			if (str[i] >= '0' && str[i] <= '9')
				return true;

		return false;
	}
}
