using CSharpFunctionalExtensions;
using FluentAssertions;
using Xunit;

namespace SimpleValidatorBuilder.Tests;

public class CustomValidatorTests
{
    [Fact]
    public void Validation_using_custom_validator_adds_data_to_error()
    {
        // Arrange
        const int exactLength = 12;
        var testString = new string('a', exactLength - 1);
        const string error = "error";

        CustomValidator<string> sut = Validate.That<string, string>()
            .LengthIsExactly(exactLength, invalidValue => error);

        // Act
        var result = sut.Validate(testString);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}

public class CustomValidator<TValue> : Validator<TValue, string>
{
    private CustomValidator(ValidatorBuilder<TValue, string> validatorBuilder) : 
        base(validatorBuilder)
    {
    }

    public Result<TValue, string> Validate(TValue value) 
        => base.Validate(value);

    public static implicit operator CustomValidator<TValue>(ValidatorBuilder<TValue, string> validatorBuilder)
        => new CustomValidator<TValue>(validatorBuilder);
}
