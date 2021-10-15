using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleValidatorBuilder.Parser;

namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore.Internal.Extensions;

internal static class PropertyBuilderExtensions
{
    public static void ApplyPropertyConfigurations<TValue, TError>(
        this PropertyBuilder propertyBuilder,
        Validator<TValue, TError> validator)
    {
        foreach (var parser in validator.Parsers)
        {
            propertyBuilder.ApplyPropertyConfiguration(parser);
        }
    }

    public static void ApplyPropertyConfiguration<TValue, TError>(this PropertyBuilder propertyBuilder, IParser<TValue, TError> parser)
    {
        if (parser is StringIsNotNullOrEmpty<TError>)
        {
            propertyBuilder.IsRequired();
        }
        else if (parser is StringContainsOnlyAlphabetCharacters<TError> || parser is StringContainsOnlyNumbers<TError>)
        {
            propertyBuilder.IsUnicode(false);
        }
        else if (parser is LengthIsLessThanOrEqual<TError> lengthIsLessThanOrEqual)
        {
            propertyBuilder.HasMaxLength(lengthIsLessThanOrEqual.MaxLength);
        }
        else if (parser is LengthIsExactly<TError> lengthIsExactly)
        {
            propertyBuilder
                .HasMaxLength(lengthIsExactly.ExactLength)
                .IsFixedLength();
        }
        else if (parser is ValueIsLessThanOrEqual<decimal, TError> decimalIsLessThanOrEqual)
        {
            var (precision, scale) = decimalIsLessThanOrEqual.MaxValue.GetPrecisionAndScale();
            propertyBuilder.HasPrecision(precision, scale);
        }
    }
}
