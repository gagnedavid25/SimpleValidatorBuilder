namespace SimpleValidatorBuilder.Internal;

internal interface IParser<TValue, TError>
{
    void Parse(ParserResult<TValue?, TError> result);
}
