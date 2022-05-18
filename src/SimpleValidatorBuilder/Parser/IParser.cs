namespace SimpleValidatorBuilder.Parser;

public interface IParser<TValue, TError>
{
	void Parse(ParserResult<TValue?, TError> result);
}
