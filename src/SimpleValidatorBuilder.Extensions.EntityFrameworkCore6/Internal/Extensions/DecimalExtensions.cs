namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore6.Internal.Extensions;

internal static class DecimalExtensions
{
    public static (int precision, int scale) GetPrecisionAndScale(this decimal value)
    {
        var stringValue = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var precision = stringValue
            .Replace(".", string.Empty)
            .Count();
        var scale = stringValue
            .SkipWhile(c => c != '.')
            .Skip(1)
            .Count();

        return (precision, scale);
    }
}
