using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class StringIsNotNullOrEmptyTests
{
    [Fact]
    public void With_non_empty_string()
    {
        // Arrange
        const int length = 12;
        var testString = new string('a', length);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsNotNullOrEmpty(_ => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testString);
    }

    [Fact]
    public void With_empty_string()
    {
        // Arrange
        var testString = string.Empty;
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsNotNullOrEmpty(_ => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void With_null_string()
    {
        // Arrange
        string? testString = null;
        var error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsNotNullOrEmpty(_ => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
