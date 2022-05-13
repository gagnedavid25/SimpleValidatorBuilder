namespace SimpleValidatorBuilder.Internal.Parser;

internal sealed class StringIsNotNullOrEmpty<TError> : IParser<string, TError>
{
    private readonly Func<string?, TError> _errorFactory;

    internal StringIsNotNullOrEmpty(Func<string?, TError> errorFactory)
        => _errorFactory = errorFactory;

    public void Parse(ParserResult<string?, TError> result)
    {
        if (string.IsNullOrEmpty(result.Value))
            result.SetError(_errorFactory(result.Value));
    }
}
