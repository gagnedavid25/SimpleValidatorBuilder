namespace SimpleValidatorBuilder.Parser;

public sealed class StringContainsOnlyNumbers<TError> : IParser<string, TError>
{
	private readonly Func<string?, TError> _errorFactory;

	internal StringContainsOnlyNumbers(Func<string?, TError> errorFactory)
		=> _errorFactory = errorFactory;

	public void Parse(ParserResult<string?, TError> result)
	{
		if (!IsDigitsOnly(result.Value))
			result.SetError(_errorFactory(result.Value));
	}

	public static bool IsDigitsOnly(string? str)
	{
		if (string.IsNullOrEmpty(str))
			return false;

		for (int i = 0; i < str.Length; i++)
			if (str[i] < '0' || str[i] > '9')
				return false;

		return true;
	}
}
