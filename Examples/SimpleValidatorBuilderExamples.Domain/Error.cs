namespace SimpleValidatorBuilderExamples.Domain;

public class Error : IEquatable<string>
{
    public string Code { get; }
    public string? InvalidElement { get; private set; }
    public List<string> MetaData { get; } = new List<string>();

    internal Error(string code) 
        => Code = code;

    public Error WithInvalidElement(string? invalidElement)
    {
        InvalidElement = invalidElement;
        return this;
    }

    public Error AddMetaData(params string[] metaData)
    {
        MetaData.AddRange(metaData);
        return this;
    }

    public bool Equals(string? code)
    {
        if (code is null)
            return false;

        return Code == code;
    }

    public override bool Equals(object? obj)
    {
        if (obj is string code)
            return Equals(code);

        return base.Equals(obj);
    }

    public override int GetHashCode() 
        => Code.GetHashCode();

    public static bool operator ==(Error? left, string? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Error? left, string? right) 
        => !(left == right);
}

public static class Errors
{
    public static Error ValueIsRequired(string? invalidElement = null, string? details = null)
        => new Error(nameof(ValueIsRequired))
            .WithInvalidElement(invalidElement)
            .AddMetaData(details ?? "Value is required");

    public static Error ValueIsInvalid(string? invalidElement = null, string? details = null)
        => new Error(nameof(ValueIsInvalid))
            .WithInvalidElement(invalidElement)
            .AddMetaData(details ?? "Value is invalid");

    public static Error LengthIsInvalid(string? invalidElement = null, string? details = null)
        => new Error(nameof(LengthIsInvalid))
            .WithInvalidElement(invalidElement)
            .AddMetaData(details ?? "Length is invalid");

    public static Error WithAttemptedValue<TValue>(this Error error, TValue attemptedValue)
    {
        string valueString;

        if (attemptedValue is null)
        {
            valueString = "null";
        }
        else if (attemptedValue is string val)
        {
            valueString = $"'{attemptedValue}'";

            if (error == nameof(Errors.LengthIsInvalid))
                error.AddMetaData($"Attempted value length: {val.Length}");
        }
        else
        {
            valueString = attemptedValue.ToString() ?? "null";
        }

        return error.AddMetaData($"Attempted value: {valueString}");
    }
}