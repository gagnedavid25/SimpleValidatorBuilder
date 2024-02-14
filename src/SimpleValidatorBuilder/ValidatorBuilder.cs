using SimpleValidatorBuilder.Parser;

namespace SimpleValidatorBuilder;

public class ValidatorBuilder<TValue, TError>
{
    private readonly List<IParser<TValue, TError>> _parsers;
    
    public IParser<TValue, TError>[] Parsers
        => _parsers.ToArray();

    internal ValidatorBuilder()
        => _parsers = new List<IParser<TValue, TError>>();

    internal void AddParser(IParser<TValue, TError> parser)
        => _parsers.Add(parser);

    internal void AddParsers(IParser<TValue, TError>[] parsers)
        => _parsers.AddRange(parsers);

    public Validator<TValue, TError> Build()
        => new Validator<TValue, TError>(Parsers);
}
