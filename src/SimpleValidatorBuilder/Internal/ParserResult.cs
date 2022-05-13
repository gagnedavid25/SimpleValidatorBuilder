namespace SimpleValidatorBuilder.Internal
{
    internal class ParserResult<TValue, TError>
    {
        public TValue? Value { get; private set; }
        public TError? Error { get; private set; }

        public bool IsFailure { get; private set; }

        public ParserResult(TValue value) 
            => SetValue(value);

        public ParserResult(TError error) 
            => SetError(error);

        public ParserResult<TValue, TError> SetValue(TValue value)
        {
            Value = value;
            IsFailure = false;

            return this;
        }

        public ParserResult<TValue, TError> SetError(TError error)
        {
            Error = error;
            IsFailure = true;

            return this;
        }
    }
}
