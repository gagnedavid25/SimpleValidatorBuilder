using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Parser;

public class StringContainsOnlyAlphabetCharacters<TError> : IParser<string, TError>
{
    public TError Error { get; }

    internal StringContainsOnlyAlphabetCharacters(TError error)
        => Error = error;

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, Error);

    public static Result<string, TError> RunValidation(in string value, in TError error)
        => !IsAlphabetCharactersOnly(value) ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);

    public static bool IsAlphabetCharactersOnly(string str)
    {
        var regex = new Regex("^[a-zA-Z]+$");
        return regex.IsMatch(str);
    }
}
