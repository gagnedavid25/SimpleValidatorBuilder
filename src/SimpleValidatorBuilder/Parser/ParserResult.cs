namespace SimpleValidatorBuilder.Parser
{
	public class ParserResult<TValue, TError>
	{
		public TValue? Value { get; private set; }
		public TError? Error { get; private set; }

		public bool IsFailure { get; private set; }

		public ParserResult(TValue value)
			=> SetValue(value);

		public ParserResult(TError error)
			=> SetError(error);

		internal ParserResult<TValue, TError> SetValue(TValue value)
		{
			Value = value;
			IsFailure = false;

			return this;
		}

		internal ParserResult<TValue, TError> SetError(TError error)
		{
			Error = error;
			IsFailure = true;

			return this;
		}
	}
}
