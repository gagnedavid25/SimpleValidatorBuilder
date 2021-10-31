﻿using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilder.Extensions.EntityFrameworkCore6.Internal.Extensions;
using System.Reflection;

namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore6;

public static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ApplyPropertiesConfigurationsUsingStaticValidator(
        this ModelConfigurationBuilder configurationBuilder,
        Assembly assembly)
    {
        var publicNonNestedNonGenericClass = assembly
            .GetTypes()
            .Where(t => t.IsPublic && !t.IsNested && !t.IsGenericType && t.IsUserDefinedClass())
            .ToArray();

        foreach (var userClass in publicNonNestedNonGenericClass)
        {
            var validatorProperties = userClass.GetStaticValidatorFields();

            if (validatorProperties.Length != 1) // Currently only supports classes with one validator builder field
                continue;

            dynamic validator = (dynamic)validatorProperties[0]
                .GetValue(null)!;

            var propertiesBuilder = configurationBuilder.Properties(userClass);
            PropertiesConfigurationBuilderExtensions.ApplyPropertiesConfigurations(propertiesBuilder, validator);
        }

        return configurationBuilder;
    }
}
