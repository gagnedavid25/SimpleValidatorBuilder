using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleValidatorBuilder.Extensions.EntityFrameworkCore.Internal.Extensions;

namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore;

public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// For all the entity user-defined class properties (value object properties), 
    /// check if there is one public <see cref="Validator{TValue, TError}"/> static field and apply the EF entity property configuration.
    /// EF value converters must be applied before calling this method.
    /// </summary>
    public static EntityTypeBuilder<TEntity> ApplyPropertiesConfigurationsUsingStaticValidator<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder)
        where TEntity : class
    {
        var entityProperties = entityTypeBuilder.Metadata
            .GetProperties()
            .ToList();

        foreach (var entityProperty in entityProperties)
        {
            var propertyType = entityProperty.ClrType;

            if (!propertyType.IsUserDefinedClass())
                continue;

            var validatorProperties = propertyType.GetStaticValidatorFields();

            if (validatorProperties.Count != 1) // Currently only supports classes with one validator builder field
                continue;

            dynamic validator = (dynamic)validatorProperties
                .First()
                .GetValue(null)!;

            var propertyBuilder = entityTypeBuilder.Property(entityProperty.Name);
            PropertyBuilderExtensions.ApplyPropertyConfigurations(propertyBuilder, validator);
        }

        return entityTypeBuilder;
    }
}
