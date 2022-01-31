using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilder.Extensions.EntityFrameworkCore6.Internal.Extensions;
using System.Reflection;

namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore6;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyPropertiesConventionsUsingStaticValidator(
        this ModelConfigurationBuilder configurationBuilder,
        Assembly assembly)
    {
        var publicNonNestedNonGenericClass = assembly
            .GetTypes()
            .Where(t => t.IsPublic && !t.IsNested && !t.IsGenericType && t.IsUserDefinedClass())
            .ToArray();

        foreach (var userClass in publicNonNestedNonGenericClass)
        {
            var validatorFields = userClass.GetStaticValidatorFields();

            if (validatorFields.Length != 1) // Currently only supports classes with one validator field
                continue;

            dynamic validator = (dynamic)validatorFields[0].GetValue(null)!;
            var propertiesBuilder = configurationBuilder.Properties(userClass);
            
            PropertiesConfigurationBuilderExtensions.ApplyPropertiesConventions(propertiesBuilder, validator);
        }

        return configurationBuilder;
    }
}
