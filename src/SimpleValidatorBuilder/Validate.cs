namespace SimpleValidatorBuilder;

public static class Validate
{
    public static ValidatorBuilder<TValue, TError> That<TValue, TError>()
        => new ValidatorBuilder<TValue, TError>();
}
