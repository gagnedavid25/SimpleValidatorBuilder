using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class StringContainsOnlyNumbers<TError> : IParser<string, TError>
{
    public TError Error { get; }

    internal StringContainsOnlyNumbers(TError error)
        => Error = error;

    public Result<string, TError> Parse(in string value)
        => RunValidation(value, Error);

    public static Result<string, TError> RunValidation(in string value, in TError error)
        => !IsDigitsOnly(value) ? Result.Failure<string, TError>(error) : Result.Success<string, TError>(value);

    public static bool IsDigitsOnly(string str)
    {
        if (str is null || str == "")
            return false;

        for (int i = 0; i < str.Length; i++)
            if (str[i] < '0' || str[i] > '9')
                return false;

        return true;
    }
}
