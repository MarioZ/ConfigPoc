using System.Configuration;

namespace ConfigPoc.Tests
{
    public static class TestUtilities
    {
        public static T GetSection<T>(string name) where T : ConfigurationSectionBase
            => (T)ConfigurationManager.GetSection(name);
    }
}
