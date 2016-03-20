using System.Configuration;

namespace ConfigPoc
{
    public sealed class UserSection : ConfigurationSectionBase
    {
        [ConfigurationProperty("application")]
        private string Application => base.GetValue("application");

        [ConfigurationProperty("version")]
        private string Version => base.GetValue("version");

        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(UserCollection), AddItemName = "user")]
        public UserCollection Users => base.GetElement<UserCollection>("");
    }
}