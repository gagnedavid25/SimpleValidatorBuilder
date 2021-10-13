using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class LengthIsLessThanOrEqualTests
{
    [Fact]
    public void With_same_length_string()
    {
        // Arrange
        const int maxLength = 12;
        var testString = new string('a', maxLength);
        const string error = "error";

        var sut = Validate.That<string, string>()
            .LengthIsLessThanOrEqual(maxLength, error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testString);
    }

    [Fact]
    public void With_longer_length_string()
    {
        // Arrange
        const int maxLength = 12;
        var testString = new string('a', maxLength + 1);
        const string error = "error";

        var sut = Validate.That<string, string>()
            .LengthIsLessThanOrEqual(maxLength, error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
