using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder.Parser;

public class ValueTransformation<TValue, TError> : IParser<TValue, TError>
{
    public Func<TValue, TValue> TransformationPredicate { get; }

    internal ValueTransformation(Func<TValue, TValue> transformationPredicate)
        => TransformationPredicate = transformationPredicate;

    public Result<TValue, TError> Parse(in TValue value)
        => TransformationPredicate.Invoke(value);
}
