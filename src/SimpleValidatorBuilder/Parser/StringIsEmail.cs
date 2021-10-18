using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;

namespace SimpleValidatorBuilder.Parser;

public class StringIsEmail<TError> : IParser<string, TError>
{
    private readonly static EmailAddressAttribute EmailAddressAttribute = new EmailAddressAttribute();

    private Func<TError> _errorFactory;

    public StringIsEmail(Func<TError> errorFactory) 
        => _errorFactory = errorFactory;

    public Result<string, TError> Parse(in string value) 
        => !EmailAddressAttribute.IsValid(value) ? Result.Failure<string, TError>(_errorFactory.Invoke()) : Result.Success<string, TError>(value);
}
