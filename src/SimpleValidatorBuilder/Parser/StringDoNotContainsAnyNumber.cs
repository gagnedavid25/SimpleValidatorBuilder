using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class StringDoNotContainsAnyNumber<TError> : IParser<string, TError>
{
    private readonly Func<TError> _errorFactory;

    internal StringDoNotContainsAnyNumber(Func<TError> errorFactory)
        => _errorFactory = errorFactory;

    public Result<string, TError> Parse(string value)
        => ContainsNumber(value) ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);

    public static bool ContainsNumber(string str)
    {
        if (str is null || str == "")
            return false;

        for (int i = 0; i < str.Length; i++)
            if (str[i] >= '0' && str[i] <= '9')
                return true;

        return false;
    }
}
