namespace SimpleValidatorBuilder.Parser
{
	public sealed class ParserResult<TValue, TError>
	{
		public TValue? Value { get; private set; }
		public TError? Error { get; private set; }

		public bool IsFailure { get; private set; }

		internal void SetValue(TValue value)
		{
			Value = value;
			IsFailure = false;
		}

		internal void SetError(TError error)
		{
			Error = error;
			IsFailure = true;
		}
	}
}
