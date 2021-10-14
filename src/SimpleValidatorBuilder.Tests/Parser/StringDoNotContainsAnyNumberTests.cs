using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests.Parser;

public class StringDoNotContainsAnyNumberTests
{
    [Fact]
    public void With_only_alphabet_character_string()
    {
        // Arrange
        const int length = 12;
        var testString = new string('a', length);
        const string error = "error";

        var sut = Validate.That<string, string>()
            .StringDoNotContainsAnyNumber(() => error)
            .Build();

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testString);
    }

    [Fact]
    public void With_only_numbers_string()
    {
        // Arrange
        const int length = 12;
        var testString = new string('0', length);
        const string error = "error";

        var sut = Validate.That<string, string>()
            .StringDoNotContainsAnyNumber(() => error)
            .Build();

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
