using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SimpleValidatorBuilder.Parser;

public sealed class StringIsEmail<TError> : IParser<string, TError>
{
    const string Chars = "a-z0-9";
    const string SpecialChars = @"_\.\-\+";
    const string RegexPattern = @$"^(?!^[{SpecialChars}])(?!.*[{SpecialChars}]{{2}})[{Chars}{SpecialChars}]+@[{Chars}\-]+(?:\.[{Chars}]+)+$";
    
    private static readonly EmailAddressAttribute EmailAddressAttribute = new EmailAddressAttribute();
    private static readonly Regex EmailRegex = new Regex(RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(300));

    private readonly Func<TError> _errorFactory;
    private readonly bool _enforceWithRegex;

    internal StringIsEmail(Func<TError> errorFactory, bool enforceWithRegex)
    {
        _errorFactory = errorFactory;
        _enforceWithRegex = enforceWithRegex;
    }

    public Result<string, TError> Parse(string value)
    {
        if (!EmailAddressAttribute.IsValid(value) || 
            _enforceWithRegex ? !EmailRegex.IsMatch(value) : false)
            return Result.Failure<string, TError>(_errorFactory.Invoke());
                
        return Result.Success<string, TError>(value);
    }
}
