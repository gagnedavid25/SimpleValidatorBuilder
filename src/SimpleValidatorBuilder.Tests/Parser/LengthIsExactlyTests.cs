using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class LengthIsExactlyTests
{
    [Fact]
    public void With_same_length_string()
    {
        // Arrange
        const int exactLength = 12;
        var testString = new string('a', exactLength);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .LengthIsExactly(exactLength, _ => error);
        
        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testString);
    }

    [Fact]
    public void With_different_length_string()
    {
        // Arrange
        const int exactLength = 12;
        var testString = new string('a', exactLength - 1);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .LengthIsExactly(exactLength, _ => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
