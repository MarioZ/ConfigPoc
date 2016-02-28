using System.Configuration;

namespace ConfigPoc
{
    public sealed class UserSection : ConfigurationSectionBase
    {
        [ConfigurationProperty("application")]
        private string Application { get { return base.GetValue("application"); } }

        [ConfigurationProperty("version")]
        private string Version { get { return base.GetValue("version"); } }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(UserCollection), AddItemName = "user")]
        public UserCollection Users { get { return base.GetElement<UserCollection>(""); } }
    }
}
