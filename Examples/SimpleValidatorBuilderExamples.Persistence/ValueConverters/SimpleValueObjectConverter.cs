using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleValidatorBuilderExamples.Domain;
using System.Reflection;

namespace SimpleValidatorBuilderExamples.Persistence.ValueConverters;

internal class SimpleValueObjectConverter<TSimpleValueObject, TValue> : ValueConverter<TSimpleValueObject, TValue>
    where TSimpleValueObject : SimpleValueObject<TValue>
{
    private static readonly Func<TValue, Result<TSimpleValueObject, Error>> FactoryMethodDelegate =
        (Func<TValue, Result<TSimpleValueObject, Error>>)Delegate.CreateDelegate(
            typeof(Func<TValue, Result<TSimpleValueObject, Error>>), 
            SimpleValueObjectConverter.GetStaticFactoryMethod(typeof(TSimpleValueObject)));

    public SimpleValueObjectConverter()
        : base(vo => vo.Value, value => SimpleValueObjectFactory(value))
    {
    }

    private static TSimpleValueObject SimpleValueObjectFactory(TValue value)
    {
        var result = FactoryMethodDelegate(value!);
        return result.Value;
    }
}

internal static class SimpleValueObjectConverter
{
    public const string StaticFactoryMethodName = "Create";

    public static readonly Type SimpleValueObjectType = typeof(SimpleValueObject<>);
    public static readonly Type ConverterType = typeof(SimpleValueObjectConverter<,>);

    public static bool IsSimpleValueObjectType(this Type type)
        => type.IsPublic && type.IsClass && !type.IsNested &&
            (type.BaseType?.IsGenericType ?? false) &&
            type.BaseType.GetGenericTypeDefinition() == SimpleValueObjectType;

    public static MethodInfo GetStaticFactoryMethod(Type simpleValueObjectType)
    {
        var factoryMethod = simpleValueObjectType.GetMethod(StaticFactoryMethodName, BindingFlags.Public | BindingFlags.Static);

        if (factoryMethod is null)
            ThrowStaticFactoryMethodMissing(simpleValueObjectType.Name);
        
        return factoryMethod!;
    }

    public static void ThrowStaticFactoryMethodMissing(string simpleValueObjectName)
        => throw new InvalidOperationException($"The value object '{simpleValueObjectName}' must have the static factory method 'Result<TSimpleValueObject, Error> {StaticFactoryMethodName}(TValue value)'.");
}
