using System.Reflection;

namespace SimpleValidatorBuilder.Extensions.EntityFrameworkCore.Internal.Extensions;

internal static class TypeExtensions
{
    public static bool IsUserDefinedClass(this Type type)
    {
        var @namespace = type.Namespace ?? "";

        if (@namespace.StartsWith("System"))
            return false;

        return type.IsClass;
    }

    public static Type GetUnderlyingNullableTypeOrItself(this Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            return Nullable.GetUnderlyingType(type)!;

        return type;
    }

    public static bool IsAssignableToGenericType(this Type type, Type genericType)
    {
        var interfaceTypes = type.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                return true;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
            return true;

        var baseType = type.BaseType;
        if (baseType is null) return false;

        return baseType.IsAssignableToGenericType(genericType);
    }

    public static FieldInfo[] GetStaticValidatorFields(this Type type)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        return fields
            .Where(f => f.FieldType.IsAssignableToGenericType(typeof(Validator<,>)))
            .ToArray();
    }
}
