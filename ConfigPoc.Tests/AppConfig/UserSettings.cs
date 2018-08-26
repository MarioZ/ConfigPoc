using System.Configuration;

namespace ConfigPoc.Tests
{
    internal sealed class UserSection : ConfigurationSectionBase
    {
        [ConfigurationProperty(nameof(Application))]
        public string Application => base.GetValue();

        [ConfigurationProperty(nameof(Version))]
        public string Version => base.GetValue();

        [ConfigurationProperty(nameof(User))]
        public UserElement User => base.GetElement<UserElement>();
    }

    internal sealed class UserElement : ConfigurationElementBase
    {
        [ConfigurationProperty(nameof(FullName))]
        public string FullName => base.GetValue();

        [ConfigurationProperty(nameof(FirstName))]
        public string FirstName => base.GetValue();

        [ConfigurationProperty(nameof(LastName))]
        public string LastName => base.GetValue();

        [ConfigurationProperty(nameof(WelcomeMessage))]
        public string WelcomeMessage => base.GetValue();
    }
}
