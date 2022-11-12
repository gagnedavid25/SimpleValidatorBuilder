using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace SimpleValidatorBuilderExamples.Persistence.ValueConverters;

internal class SimpleValueObjectConverter<TSimpleValueObject, TValue> : ValueConverter<TSimpleValueObject, TValue>
    where TSimpleValueObject : SimpleValueObject<TValue>
{
    private static readonly Func<TValue, TSimpleValueObject> FactoryMethodDelegate =
        (Func<TValue, TSimpleValueObject>)Delegate.CreateDelegate(
            typeof(Func<TValue, TSimpleValueObject>), 
            SimpleValueObjectConverter.GetStaticFactoryMethod(typeof(TSimpleValueObject), typeof(TValue)));

    public SimpleValueObjectConverter()
        : base(vo => vo.Value, value => FactoryMethodDelegate(value))
    {
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

    public static MethodInfo GetStaticFactoryMethod(Type simpleValueObjectType, Type valueType)
    {
        var parametersTypes = new Type[] { valueType };
        var factoryMethod = simpleValueObjectType.GetMethod(StaticFactoryMethodName, BindingFlags.Public | BindingFlags.Static, parametersTypes);

        if (factoryMethod is null)
            ThrowStaticFactoryMethodMissing(simpleValueObjectType.Name);
        
        return factoryMethod!;
    }

    public static void ThrowStaticFactoryMethodMissing(string simpleValueObjectName)
        => throw new InvalidOperationException($"The value object '{simpleValueObjectName}' must have the static factory method 'Result<TSimpleValueObject, Error> {StaticFactoryMethodName}(TValue value)'.");
}
