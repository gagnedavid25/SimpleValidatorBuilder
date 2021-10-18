using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class StringIsEmailTests
{
    [Fact]
    public void With_valid_email()
    {
        // Arrange
        const string validEmail = "test@simplevalidatorbuilder.com";
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsEmail(() => error);

        // Act
        var result = sut.Validate(validEmail);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(validEmail);
    }

    [Fact]
    public void With_invalid_email()
    {
        // Arrange
        const string invalidEmail = "@simplevalidatorbuilder.com";
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsEmail(() => error);

        // Act
        var result = sut.Validate(invalidEmail);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
