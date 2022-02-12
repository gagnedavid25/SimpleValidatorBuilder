using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Parser;

public sealed class StringContainsOnlyAlphabetCharacters<TError> : IParser<string, TError>
{
    private readonly Func<TError> _errorFactory;

    internal StringContainsOnlyAlphabetCharacters(Func<TError> errorFactory)
        => _errorFactory = errorFactory;

    public Result<string, TError> Parse(string value)
        => !IsAlphabetCharactersOnly(value) ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);

    public static bool IsAlphabetCharactersOnly(string str) 
        => RegexAlphabetCharactersOnly.IsMatch(str);

    private static readonly Regex RegexAlphabetCharactersOnly = 
        new Regex("^[a-z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(300));
}
