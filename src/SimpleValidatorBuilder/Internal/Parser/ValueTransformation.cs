namespace SimpleValidatorBuilder.Internal.Parser;

internal sealed class ValueTransformation<TValue, TError> : IParser<TValue, TError>
{
    private readonly Func<TValue?, TValue?> _transformationPredicate;

    internal ValueTransformation(Func<TValue?, TValue?> transformationPredicate)
        => _transformationPredicate = transformationPredicate;

    public void Parse(ParserResult<TValue?, TError> result)
        => result.SetValue(_transformationPredicate(result.Value));
}
