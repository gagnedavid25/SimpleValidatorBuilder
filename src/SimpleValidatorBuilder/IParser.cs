﻿using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilder;

public interface IParser<TValue, TError>
{
    Result<TValue, TError> Parse(in TValue value);
}
