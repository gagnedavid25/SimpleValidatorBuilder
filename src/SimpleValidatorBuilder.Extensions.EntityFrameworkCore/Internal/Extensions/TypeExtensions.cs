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

    public static IReadOnlyList<FieldInfo> GetStaticValidatorBuilderFields(this Type type)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        return fields
            .Where(f =>
                f.FieldType.IsGenericType &&
                f.FieldType.GetGenericTypeDefinition() == typeof(ValidatorBuilder<,>))
            .ToList();
    }
}
