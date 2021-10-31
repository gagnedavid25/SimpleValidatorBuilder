using SimpleValidatorBuilder.Parser;

namespace SimpleValidatorBuilder;

public static class ValidatorBuilderExtensions
{
    public static ValidatorBuilder<TValue, TError> ValueIsGreaterThanOrEqual<TValue, TError>(
        this ValidatorBuilder<TValue, TError> validatorBuilder,
        TValue minValue,
        Func<TError> errorFactory)
        where TValue : IComparable<TValue>
    {
        validatorBuilder.AddParser(new ValueIsGreaterThanOrEqual<TValue, TError>(minValue, errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<TValue, TError> ValueIsLessThanOrEqual<TValue, TError>(
        this ValidatorBuilder<TValue, TError> validatorBuilder,
        TValue maxValue,
        Func<TError> errorFactory)
        where TValue : IComparable<TValue>
    {
        validatorBuilder.AddParser(new ValueIsLessThanOrEqual<TValue, TError>(maxValue, errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringIsNotNullOrEmpty<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new StringIsNotNullOrEmpty<TError>(errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringContainsOnlyNumbers<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new StringContainsOnlyNumbers<TError>(errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringContainsOnlyAlphabetCharacters<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new StringContainsOnlyAlphabetCharacters<TError>(errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringDoNotContainsAnyNumber<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new StringDoNotContainsAnyNumber<TError>(errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> StringIsEmail<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        Func<TError> errorFactory,
        bool enforceWithRegex = false)
    {
        validatorBuilder.AddParser(new StringIsEmail<TError>(errorFactory, enforceWithRegex));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> LengthIsExactly<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        int exactLength,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new LengthIsExactly<TError>(exactLength, errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> LengthIsGreaterThanOrEqual<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        int minLength,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new LengthIsGreaterThanOrEqual<TError>(minLength, errorFactory));
        return validatorBuilder;
    }

    public static ValidatorBuilder<string, TError> LengthIsLessThanOrEqual<TError>(
        this ValidatorBuilder<string, TError> validatorBuilder,
        int maxLength,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new LengthIsLessThanOrEqual<TError>(maxLength, errorFactory));
        return validatorBuilder;
    }

    /// <summary>
    /// Use this method only if no other method meets your needs
    /// </summary>
    public static ValidatorBuilder<TValue, TError> ValueMust<TValue, TError>(
        this ValidatorBuilder<TValue, TError> validatorBuilder,
        Func<TValue, bool> valuePredicate,
        Func<TError> errorFactory)
    {
        validatorBuilder.AddParser(new ValueMust<TValue, TError>(valuePredicate, errorFactory));
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
