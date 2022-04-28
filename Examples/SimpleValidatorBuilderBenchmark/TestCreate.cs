using BenchmarkDotNet.Attributes;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;

namespace SimpleValidatorBuilderBenchmark;

[MemoryDiagnoser]
public class TestCreate
{
    public const string FirstName = "David";

    [Benchmark]
    public void Create_with_validator_builder()
    {
        var firstName = PersonName.Create(FirstName);
    }

    [Benchmark]
    public void Create_without_validator_builder()
    {
        var firstName = PersonNameWithoutValidator.Create(FirstName);
    }
}
