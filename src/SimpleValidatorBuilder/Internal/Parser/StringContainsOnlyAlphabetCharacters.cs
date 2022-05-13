using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Internal.Parser;

internal sealed class StringContainsOnlyAlphabetCharacters<TError> : IParser<string, TError>
{
    private readonly Func<string?, TError> _errorFactory;

    internal StringContainsOnlyAlphabetCharacters(Func<string?, TError> errorFactory)
        => _errorFactory = errorFactory;

    public void Parse(ParserResult<string?, TError> result)
    {
        if (!IsAlphabetCharactersOnly(result.Value))
            result.SetError(_errorFactory(result.Value));
    }

    public static bool IsAlphabetCharactersOnly(string? str)
        => RegexAlphabetCharactersOnly.IsMatch(str ?? "");

    private static readonly Regex RegexAlphabetCharactersOnly =
        new Regex("^[a-z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(300));
}
