using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class StringContainsOnlyNumbers<TError> : IParser<string, TError>
{
    public Func<TError> ErrorFactory { get; }

    internal StringContainsOnlyNumbers(Func<TError> errorFactory)
        => ErrorFactory = errorFactory;

    public Result<string, TError> Parse(in string value)
        => !IsDigitsOnly(value) ? Result.Failure<string, TError>(ErrorFactory.Invoke()) : Result.Success<string, TError>(value);

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
