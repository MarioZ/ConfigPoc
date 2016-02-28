using System;
using System.Configuration;

namespace ConfigPoc
{
    public sealed class UserElement : ConfigurationElementBase
    {
        public override string Key { get { return this.Name; } }

        [ConfigurationProperty("name")]
        private string Name { get { return base.GetValue("name"); } }

        [ConfigurationProperty("firstName")]
        private string FirstName { get { return base.GetValue("firstName"); } }

        [ConfigurationProperty("lastName")]
        private string LastName { get { return base.GetValue("lastName"); } }

        [ConfigurationProperty("lastVisited")]
        public DateTime LastVisited { get { return (DateTime)base["lastVisited"]; } }

        [ConfigurationProperty("credits")]
        public double Credits { get { return (double)base["credits"]; } }

        [ConfigurationProperty("messages")]
        public MessagesElement Messages { get { return base.GetElement<MessagesElement>("messages"); } }
    }
}
