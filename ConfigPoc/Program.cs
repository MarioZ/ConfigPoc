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
                Console.WriteLine($"{user.Messages.Welcome}\n{user.Messages.Info}\n");

            Console.Read();
        }
    }
}