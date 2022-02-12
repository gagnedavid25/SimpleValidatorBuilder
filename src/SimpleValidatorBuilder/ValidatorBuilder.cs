namespace SimpleValidatorBuilder;

public class ValidatorBuilder<TValue, TError>
{
    private readonly List<IParser<TValue, TError>> _parsers;
    public IReadOnlyList<IParser<TValue, TError>> Parsers
        => _parsers.AsReadOnly();

    internal ValidatorBuilder()
        => _parsers = new List<IParser<TValue, TError>>();

    public void AddParser(IParser<TValue, TError> validation)
        => _parsers.Add(validation);

    public Validator<TValue, TError> Build()
        => new Validator<TValue, TError>(this);
}
