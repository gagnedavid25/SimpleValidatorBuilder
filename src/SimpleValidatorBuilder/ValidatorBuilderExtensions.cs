using SimpleValidatorBuilder.Parser;

namespace SimpleValidatorBuilder;

public static class ValidatorBuilderExtensions
{
    public static ValidatorBuilder<TValue, TError> ValueIsGreaterThanOrEqual<TValue, TError>(
        this ValidatorBuilder<TValue, TError> validatorBuilder,
        TValue minValue,
        TError error)
        where TValue : IComparable<TValue>
    {
        validatorBuilder.AddParser(new ValueIsGreaterThanOrEqual<TValue, TError>(minValue, error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<decimal, TError> ValueIsGreaterThanOrEqual<TError>(
        this ValidatorBuilder<decimal, TError> validatorBuilder,
        decimal minValue,
        TError error)
    {
        validatorBuilder.AddParser(new DecimalIsGreaterThanOrEqual<TError>(minValue, error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<TValue, TError> ValueIsLessThanOrEqual<TValue, TError>(
        this ValidatorBuilder<TValue, TError> validatorBuilder,
        TValue maxValue,
        TError error)
        where TValue : IComparable<TValue>
    {
        validatorBuilder.AddParser(new ValueIsLessThanOrEqual<TValue, TError>(maxValue, error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<decimal, TError> ValueIsLessThanOrEqual<TError>(
        this ValidatorBuilder<decimal, TError> validatorBuilder,
        decimal maxValue,
        TError error)
    {
        validatorBuilder.AddParser(new DecimalIsLessThanOrEqual<TError>(maxValue, error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringIsNotNullOrEmpty<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        TError error)
    {
        validatorBuilder.AddParser(new StringIsNotNullOrEmpty<TError>(error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringContainsOnlyNumbers<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        TError error)
    {
        validatorBuilder.AddParser(new StringContainsOnlyNumbers<TError>(error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringContainsOnlyAlphabetCharacters<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        TError error)
    {
        validatorBuilder.AddParser(new StringContainsOnlyAlphabetCharacters<TError>(error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringDoNotContainsAnyNumber<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        TError error)
    {
        validatorBuilder.AddParser(new StringDoNotContainsAnyNumber<TError>(error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> LengthIsExactly<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        int exactLength,
        TError error)
    {
        validatorBuilder.AddParser(new LengthIsExactly<TError>(exactLength, error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> LengthIsLessThanOrEqual<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        int maxLength,
        TError error)
    {
        validatorBuilder.AddParser(new LengthIsLessThanOrEqual<TError>(maxLength, error));
        return validatorBuilder;
    }

    public static ValidatorBuilder<TValue, TError> WithTransformation<TValue, TError>(
        this ValidatorBuilder<TValue, TError> validatorBuilder,
        Func<TValue, TValue> transformationPredicate)
    {
        validatorBuilder.AddParser(new ValueTransformation<TValue, TError>(transformationPredicate));
        return validatorBuilder;
    }
}
