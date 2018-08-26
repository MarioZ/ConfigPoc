using System.Configuration;
using System.Runtime.CompilerServices;


namespace ConfigPoc
{
    internal static class ConfigurationParents
    {
        private static readonly ConditionalWeakTable<ConfigurationElement, ConfigurationElementParent> Parents =
            new ConditionalWeakTable<ConfigurationElement, ConfigurationElementParent>();

        public static void SetParent(this ConfigurationElement element, ConfigurationElement parent)
            => Parents.GetOrCreateValue(element).Value = parent;

        public static ConfigurationElement GetParent(this ConfigurationElement element)
            => Parents.GetOrCreateValue(element).Value;

        private sealed class ConfigurationElementParent
        {
            public ConfigurationElement Value { get; set; }
        }
    }
}
