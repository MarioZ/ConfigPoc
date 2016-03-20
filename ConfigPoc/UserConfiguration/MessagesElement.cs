using System.Configuration;

namespace ConfigPoc
{
    public sealed class MessagesElement : ConfigurationElementBase
    {
        [ConfigurationProperty("welcome")]
        public string Welcome => base.GetValue("welcome");

        [ConfigurationProperty("info")]
        public string Info => base.GetValue("info");
    }
}