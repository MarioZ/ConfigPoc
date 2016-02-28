using System;
using System.Configuration;

namespace ConfigPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            UserSection userSettings = (UserSection)ConfigurationManager.GetSection("userSection");

            foreach (var user in userSettings.Users)
                Console.WriteLine("{1}{0}{2}{0}",
                    Environment.NewLine,
                    user.Messages.Welcome,
                    user.Messages.Info);

            Console.Read();
        }
    }
}