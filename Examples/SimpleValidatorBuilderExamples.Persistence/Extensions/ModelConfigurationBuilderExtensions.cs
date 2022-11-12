using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilderExamples.Persistence.ValueConverters;
using System.Reflection;

namespace SimpleValidatorBuilderExamples.Persistence.Extensions;

internal static class ModelConfigurationBuilderExtensions
{
    public static ModelConfigurationBuilder ConfigureSimpleValueObjects(this ModelConfigurationBuilder configurationBuilder, Assembly assembly)
    {
        var simpleValueObjectTypes = assembly
            .GetTypes()
            .Where(t => t.IsSimpleValueObjectType())
            .ToArray();

        foreach (var type in simpleValueObjectTypes)
        {
            var converterType = SimpleValueObjectConverter.ConverterType.MakeGenericType(type, type.BaseType!.GetGenericArguments()[0]);

            configurationBuilder
                .Properties(type)
                .HaveConversion(converterType);
        }

        return configurationBuilder;
    }
}
