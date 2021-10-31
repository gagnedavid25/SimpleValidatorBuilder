using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleValidatorBuilderExamples.Domain;
using System.Reflection;

namespace SimpleValidatorBuilderExamples.Persistence.ValueConverters;

internal class SimpleValueObjectConverter<TSimpleValueObject, TValue> : ValueConverter<TSimpleValueObject, TValue>
    where TSimpleValueObject : SimpleValueObject<TValue>
{
    public SimpleValueObjectConverter()
        : base(vo => vo.Value, value => SimpleValueObjectFactory(value))
    {
    }

    public static TSimpleValueObject SimpleValueObjectFactory(TValue value)
    {
        var factoryMethodInfo = SimpleValueObjectConverter.GetStaticFactoryMethod(typeof(TSimpleValueObject));

        if (factoryMethodInfo is null)
            SimpleValueObjectConverter.ThrowStaticFactoryMethodMissing(typeof(TSimpleValueObject).Name);

        var result = (Result<TSimpleValueObject, Error>)factoryMethodInfo.Invoke(null, new object[] { value! })!;
        return result.Value;
    }
}

internal static class SimpleValueObjectConverter
{
    public const string StaticFactoryMethodName = "Create";

    public static readonly Type TypeToSearch = typeof(SimpleValueObject<>);
    public static readonly Type ConverterType = typeof(SimpleValueObjectConverter<,>);

    public static bool IsSimpleValueObjectType(this Type type)
        => type.IsPublic && type.IsClass && !type.IsNested &&
            (type.BaseType?.IsGenericType ?? false) &&
            type.BaseType.GetGenericTypeDefinition() == TypeToSearch;

    public static MethodInfo? GetStaticFactoryMethod(Type simpleValueObjectType)
        => simpleValueObjectType.GetMethod(StaticFactoryMethodName, BindingFlags.Public | BindingFlags.Static);

    public static void ThrowStaticFactoryMethodMissing(string simpleValueObjectName)
        => throw new InvalidOperationException($"The value object '{simpleValueObjectName}' must have the static factory method '{StaticFactoryMethodName}'.");
}
