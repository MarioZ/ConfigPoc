using System;
using System.Configuration;

namespace ConfigPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            /* RESULT:
             * Hi John Doe,
             * your last visit was on March 30 2016,
             * your current credit is 10,203,405! */
            UserSection userSettings = (UserSection)ConfigurationManager.GetSection("userSettings");
            string message = userSettings.Message;
            Console.WriteLine(message);
            
            Console.Read();
        }
    }

    #region UserSection Sample
    public sealed class UserSection : ConfigurationSectionBase
    {
        [ConfigurationProperty("firstName")]
        private string FirstName { get { return base.GetValue("firstName"); } }

        [ConfigurationProperty("lastName")]
        private string LastName { get { return base.GetValue("lastName"); } }

        [ConfigurationProperty("lastVisited")]
        private string LastVisited { get { return base.GetValue("lastVisited"); } }

        [ConfigurationProperty("credits")]
        private string Credits { get { return base.GetValue("credits"); } }

        [ConfigurationProperty("message")]
        public string Message { get { return base.GetValue("message"); } }
    }
    #endregion
}