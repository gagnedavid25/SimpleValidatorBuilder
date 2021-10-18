using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Parser;

public sealed class StringContainsOnlyAlphabetCharacters<TError> : IParser<string, TError>
{
    private Func<TError> _errorFactory;

    internal StringContainsOnlyAlphabetCharacters(Func<TError> errorFactory)
        => _errorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => !IsAlphabetCharactersOnly(value) ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);

    public static bool IsAlphabetCharactersOnly(string str) 
        => RegexAlphabetCharactersOnly.IsMatch(str);

    private readonly static Regex RegexAlphabetCharactersOnly = 
        new Regex("^[a-z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(300));
}
