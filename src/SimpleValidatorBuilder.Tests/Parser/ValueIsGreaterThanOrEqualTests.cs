using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class ValueIsGreaterThanOrEqualTests
{
    [Fact]
    public void With_equal_value()
    {
        // Arrange
        const int minValue = 12;
        const string error = "error";

        Validator<int, string> sut = Validate.That<int, string>()
            .ValueIsGreaterThanOrEqual(minValue, invalidValue => error);

        // Act
        var result = sut.Validate(minValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(minValue);
    }

    [Fact]
    public void With_smaller_value()
    {
        // Arrange
        const int minValue = 12;
        var testValue = minValue - 1;
        const string error = "error";

        Validator<int, string> sut = Validate.That<int, string>()
            .ValueIsGreaterThanOrEqual(minValue, invalidValue => error);

        // Act
        var result = sut.Validate(testValue);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
