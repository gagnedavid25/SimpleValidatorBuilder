using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public sealed class ValueTransformation<TValue, TError> : IParser<TValue, TError>
{
    private readonly Func<TValue, TValue> _transformationPredicate;

    internal ValueTransformation(Func<TValue, TValue> transformationPredicate)
        => _transformationPredicate = transformationPredicate;

    public Result<TValue, TError> Parse(in TValue value)
        => _transformationPredicate.Invoke(value);
}
