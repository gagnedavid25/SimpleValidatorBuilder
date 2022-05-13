using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class ValueIsLessThanOrEqualTests
{
    [Fact]
    public void With_equal_value()
    {
        // Arrange
        const int maxValue = 12;
        const string error = "error";

        Validator<int, string> sut = Validate.That<int, string>()
            .ValueIsLessThanOrEqual(maxValue, invalidValue => error);

        // Act
        var result = sut.Validate(maxValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(maxValue);
    }

    [Fact]
    public void With_larger_value()
    {
        // Arrange
        const int maxValue = 12;
        var testValue = maxValue + 1;
        const string error = "error";

        Validator<int, string> sut = Validate.That<int, string>()
            .ValueIsLessThanOrEqual(maxValue, invalidValue => error);

        // Act
        var result = sut.Validate(testValue);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
