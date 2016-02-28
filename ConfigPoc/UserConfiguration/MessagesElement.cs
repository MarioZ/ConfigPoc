using System.Configuration;

namespace ConfigPoc
{
    public sealed class MessagesElement : ConfigurationElementBase
    {
        [ConfigurationProperty("welcome")]
        public string Welcome { get { return base.GetValue("welcome"); } }

        [ConfigurationProperty("info")]
        public string Info { get { return base.GetValue("info"); } }
    }
}
