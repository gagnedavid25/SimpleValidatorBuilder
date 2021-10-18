# SimpleValidatorBuilder
A simple C# validator builder using a fluent interface.

This was created to help perform validations in the domain, more specifically in value objects.

## Dependencies
- [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions) created by Vladimir Khorikov

## Example

```csharp
using CSharpFunctionalExtensions;
using SimpleValidatorBuilder;

public class Celsius : SimpleValueObject<decimal>
{
    public const decimal AbsoluteZeroInCelsius = -273.15m;

    private Celsius(decimal temperatureInCelsius)
        : base(temperatureInCelsius)
    {
    }

    public static Result<Celsius, string> Create(decimal temperatureInCelsius)
    {
        var result = Validator.Validate(temperatureInCelsius);

        if (result.IsFailure)
            return result.Error;

        return new Celsius(result.Value); // Using result.Value to preserve the transformations applied
    }

    public static readonly Validator<decimal, string> Validator =
        Validate.That<decimal, string>()
            .WithTransformation(value => Decimal.Round(value, 2))
            .ValueIsGreaterThanOrEqual(AbsoluteZeroInCelsius, () => $"Temperature in celsius cannot be below the absolute zero ({AbsoluteZeroInCelsius}).");
}
```
\* The SimpleValueObject class and the Result struct are from the CSharpFunctionalExtensions package.

To create an instance of the Celsius class:
```csharp
var celsius = Celsius.Create(20m).Value;
```

To only validate if a decimal is a valid Celsius temperature:
```csharp
var result = Celsius.Validator.Validate(valueToValidate);

if (result.IsSuccess)
    // valueToValidate is a valid Celsius

if (result.IsFailure)
    // Not valid, do something with result.Error
```

# SimpleValidatorBuilder.Extensions.EntityFrameworkCore
This package provides the `ApplyPropertiesConfigurationsUsingStaticValidator` extension method to the `Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity>` class.

This method will loop through all properties of the current EntityTypeBuilder's entity, search for the static Validator<TValue, TError> field within the property's type, and apply EF configurations with it.

For example, if the validator uses the *StringIsNotNullOrEmpty* parser, `.IsRequired()` will be applied on the properties using this validator.

## Currently supported EF configurations
Parser | Applied configuration(s)
--- | ---
StringIsNotNullOrEmpty | `IsRequired()`
StringContainsOnlyAlphabetCharacters | `IsUnicode(false)`
StringContainsOnlyNumbers | `IsUnicode(false)`
LengthIsLessThanOrEqual | `HasMaxLength(MaxLengthFromParser)`
LengthIsExactly | `HasMaxLength(ExactLengthFromParser)` and `IsFixedLength()`
ValueIsLessThanOrEqual (for decimal only) | `HasPrecision(PrecisionObtainedWithMaxValueFromParser, ScaleObtainedWithMaxValueFromParser)`

