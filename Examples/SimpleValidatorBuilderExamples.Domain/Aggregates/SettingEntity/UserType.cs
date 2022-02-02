using CSharpFunctionalExtensions;

namespace SimpleValidatorBuilderExamples.Domain.Aggregates.SettingEntity
{
    public class UserType : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public bool IsAdmin { get; private set; }

        public UserType(string code, string name, bool isAdmin)
        {
            Code = code;
            Name = name;
            IsAdmin = isAdmin;
        }
    }
}
