using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class StringContainsOnlyNumbersTests
{
    [Fact]
    public void With_only_numbers_string()
    {
        // Arrange
        const int length = 12;
        var testString = new string('0', length);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringContainsOnlyNumbers(invalidValue => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testString);
    }

    [Fact]
    public void With_only_alphabet_character_string()
    {
        // Arrange
        const int length = 12;
        var testString = new string('a', length);
        const string error = "error";

        Validator<string, string> sut = Validate.That<string, string>()
            .StringContainsOnlyNumbers(invalidValue => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
