using System;
using System.Configuration;

namespace ConfigPoc
{
    public sealed class UserElement : ConfigurationElementBase
    {
        public override string Key => this.Name;

        [ConfigurationProperty("name")]
        private string Name => base.GetValue("name");

        [ConfigurationProperty("firstName")]
        private string FirstName => base.GetValue("firstName");

        [ConfigurationProperty("lastName")]
        private string LastName => base.GetValue("lastName");

        [ConfigurationProperty("lastVisited")]
        public DateTime LastVisited => (DateTime)base["lastVisited"];

        [ConfigurationProperty("credits")]
        public double Credits => (double)base["credits"];

        [ConfigurationProperty("messages")]
        public MessagesElement Messages => base.GetElement<MessagesElement>("messages");
    }
}