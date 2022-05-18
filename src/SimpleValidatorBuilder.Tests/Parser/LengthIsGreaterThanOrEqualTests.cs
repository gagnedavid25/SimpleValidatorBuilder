using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class LengthIsGreaterThanOrEqualTests
{
    [Fact]
    public void With_same_length_string()
    {
        // Arrange
        const int minLength = 12;
        var testString = new string('a', minLength);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .LengthIsGreaterThanOrEqual(minLength, _ => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testString);
    }

    [Fact]
    public void With_shorter_length_string()
    {
        // Arrange
        const int minLength = 12;
        var testString = new string('a', minLength - 1);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .LengthIsGreaterThanOrEqual(minLength, _ => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
