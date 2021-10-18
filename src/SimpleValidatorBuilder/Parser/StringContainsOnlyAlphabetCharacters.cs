using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Parser;

public class StringContainsOnlyAlphabetCharacters<TError> : IParser<string, TError>
{
    public Func<TError> ErrorFactory { get; }

    internal StringContainsOnlyAlphabetCharacters(Func<TError> errorFactory)
        => ErrorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => !IsAlphabetCharactersOnly(value) ? Result.Failure<string, TError>(ErrorFactory.Invoke()) : Result.Success<string, TError>(value);

    public static bool IsAlphabetCharactersOnly(string str) 
        => RegexAlphabetCharactersOnly.IsMatch(str);

    private readonly static Regex RegexAlphabetCharactersOnly = 
        new Regex("^[a-z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(300));
}
