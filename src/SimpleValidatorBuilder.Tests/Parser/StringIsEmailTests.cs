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

    [Theory]
    [InlineData("testregex@simplevalidatorbuilder.com")]
    [InlineData("test-regex@simple-validator-builder.com")]
    [InlineData("test.regex@simple-validator-builder.com")]
    [InlineData("test_regex@simple-validator-builder.com")]
    [InlineData("test+regex@simple-validator-builder.com")]
    [InlineData("test+regex@simple-validator-builder.yu.uk")]
    public void With_valid_email_regex(string validEmail)
    {
        // Arrange
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsEmail(() => error, true);

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

    [Theory]
    [InlineData("-testregex@simplevalidatorbuilder.com")]
    [InlineData("testregex@simplevalidatorbuilder.com-")]
    [InlineData("test--regex@simplevalidatorbuilder.com")]
    [InlineData("testregex@simplevalidatorbuilder..com")]
    [InlineData("testregex@simple_validatorbuilder.com")]
    [InlineData("testregex@simple+validatorbuilder.com")]
    [InlineData("testregex@simplevalidatorbuilder.")]
    [InlineData("testregex@simplevalidatorbuilder")]
    [InlineData("test-_regex@simplevalidatorbuilder.com")]
    public void With_invalid_email_regex(string invalidEmail)
    {
        // Arrange
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringIsEmail(() => error, true);

        // Act
        var result = sut.Validate(invalidEmail);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
