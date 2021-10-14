using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Parser;

public class StringContainsOnlyAlphabetCharacters<TError> : IParser<string, TError>
{
    public Func<TError> ErrorFactory { get; }

    internal StringContainsOnlyAlphabetCharacters(Func<TError> errorFactory)
        => ErrorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, ErrorFactory.Invoke());

    public static Result<string, TError> RunValidation(in string value, in TError error)
        => !IsAlphabetCharactersOnly(value) ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);

    public static bool IsAlphabetCharactersOnly(string str)
    {
        var regex = new Regex("^[a-zA-Z]+$");
        return regex.IsMatch(str);
    }
}
