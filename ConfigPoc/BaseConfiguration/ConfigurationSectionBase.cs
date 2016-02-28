using System.Configuration;

namespace ConfigPoc
{
    public abstract class ConfigurationSectionBase : ConfigurationSection, IInheritedConfiguration
    {
        public IInheritedConfiguration Parent { get; set; }
        protected string GetValue(string name) { return this.GetInheritedValue(name); }
        protected T GetElement<T>(string name) where T : IInheritedConfiguration { return this.GetInheritedElement<T>(name); }

        bool IInheritedConfiguration.ContainsValueCore(string name) { return base.Properties.Contains(name); }
        object IInheritedConfiguration.GetValueCore(string name) { return base[name]; }
    }
}
