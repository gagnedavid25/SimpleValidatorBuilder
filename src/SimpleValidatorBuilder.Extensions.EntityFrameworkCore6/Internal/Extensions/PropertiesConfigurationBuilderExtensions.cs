using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleValidatorBuilder.Parser;

namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore6.Internal.Extensions;

internal static class PropertiesConfigurationBuilderExtensions
{
    public static void ApplyPropertiesConfigurations<TValue, TError>(
        this PropertiesConfigurationBuilder propertiesBuilder,
        Validator<TValue, TError> validator)
    {
        foreach (var parser in validator.Parsers)
        {
            propertiesBuilder.ApplyPropertiesConfiguration(parser);
        }
    }

    public static void ApplyPropertiesConfiguration<TValue, TError>(this PropertiesConfigurationBuilder propertiesBuilder, IParser<TValue, TError> parser)
    {
        if (parser is StringContainsOnlyAlphabetCharacters<TError> || parser is StringContainsOnlyNumbers<TError>)
        {
            propertiesBuilder.AreUnicode(false);
        }
        else if (parser is LengthIsLessThanOrEqual<TError> lengthIsLessThanOrEqual)
        {
            propertiesBuilder.HaveMaxLength(lengthIsLessThanOrEqual.MaxLength);
        }
        else if (parser is LengthIsExactly<TError> lengthIsExactly)
        {
            propertiesBuilder
                .HaveMaxLength(lengthIsExactly.ExactLength)
                .AreFixedLength();
        }
        else if (parser is ValueIsLessThanOrEqual<decimal, TError> decimalIsLessThanOrEqual)
        {
            var (precision, scale) = decimalIsLessThanOrEqual.MaxValue.GetPrecisionAndScale();
            propertiesBuilder.HavePrecision(precision, scale);
        }
    }
}
