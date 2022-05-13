using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class ValueTransformationTests
{
    [Fact]
    public void With_value_transformation()
    {
        // Arrange
        const int testValue = 12;
        const int transformedValue = 6;

        Validator<int, string> sut = Validate.That<int, string>()
            .WithTransformation(value => value / 2);

        // Act
        var result = sut.Validate(testValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(transformedValue);
    }

    [Fact]
    public void Validation_should_use_transformed_value()
    {
        // Arrange
        const int maxValue = 6;
        const int testValue = 12;
        const int transformedValue = 6;
        const string error = "error";

        Validator<int, string> sut = Validate.That<int, string>()
            .WithTransformation(value => value / 2)
            .ValueIsLessThanOrEqual(maxValue, invalidValue => error);

        // Act
        var result = sut.Validate(testValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(transformedValue);
    }
}
