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
\* *The SimpleValueObject class and the Result struct are from the CSharpFunctionalExtensions package.*

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

See the [Examples](https://github.com/gagnedavid25/SimpleValidatorBuilder/tree/master/Examples) folder for a complete solution.
# Extensions
## EntityFrameworkCore
This package target EF Core 5 and provides the `ApplyPropertiesConfigurationsUsingStaticValidator` extension method to the `Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity>` class.

This method will loop through all properties of the current EntityTypeBuilder's entity, search for the static Validator<TValue, TError> field within the property's type, and apply EF configurations with it.

For example, if the validator uses the *LengthIsLessThanOrEqual* parser, `.HasMaxLength(maxLength)` will be applied on the properties using this validator.

### Currently supported EF Core 5 configurations (v1.5.0+)
Parser | Applied configuration(s)
--- | ---
StringContainsOnlyAlphabetCharacters | `IsUnicode(false)`
StringContainsOnlyNumbers | `IsUnicode(false)`
StringIsEmail | `IsUnicode(false)`
LengthIsLessThanOrEqual | `HasMaxLength(MaxLengthFromParser)`
LengthIsExactly | `HasMaxLength(ExactLengthFromParser)` and `IsFixedLength()`
ValueIsLessThanOrEqual (for decimal only) | `HasPrecision(PrecisionObtainedWithMaxValueFromParser, ScaleObtainedWithMaxValueFromParser)`

## EntityFrameworkCore6
This package target EF Core 6 and provides the `ApplyPropertiesConventionsUsingStaticValidator` extension method to the `Microsoft.EntityFrameworkCore.ModelConfigurationBuilder` class.

It uses the new [pre-convention model configuration](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew#pre-convention-model-configuration) feature introduced in EF Core 6.

This method will:
- Scan the provided assembly for all public, non nested, non generic classes that have exactly one static *Validator* field, and are not in the `System` namespace;
- Loop through all these classes and apply the properties conventions using the *Validator* field.

### Usage
```csharp
public class YourDbContext : DbContext
{
    [...]

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ApplyPropertiesConventionsUsingStaticValidator(yourAssembly);
        
        // Don't forget to specify a value converter for the classes having a Validator field.
        // This is done with `configurationBuilder.HaveConversion()`
    }
}
```

### Currently supported EF Core 6 conventions (v1.5.0+)
Parser | Applied convention(s)
--- | ---
StringContainsOnlyAlphabetCharacters | `AreUnicode(false)`
StringContainsOnlyNumbers | `AreUnicode(false)`
StringIsEmail | `AreUnicode(false)`
LengthIsLessThanOrEqual | `HaveMaxLength(MaxLengthFromParser)`
LengthIsExactly | `HaveMaxLength(ExactLengthFromParser)` and `AreFixedLength()`
ValueIsLessThanOrEqual (for decimal only) | `HavePrecision(PrecisionObtainedWithMaxValueFromParser, ScaleObtainedWithMaxValueFromParser)`
