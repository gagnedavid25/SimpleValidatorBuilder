using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity
{
    public class UserType : Entity
    {
        public string Code { get; private set; }

        internal UserType(long id)
        {
            Id = id;
        }
    }
}
