using System.Configuration;

namespace ConfigPoc
{
    public abstract class ConfigurationSectionBase : ConfigurationSection, IInheritedConfiguration
    {
        public IInheritedConfiguration Parent { get; set; }
        protected string GetValue(string name) => this.GetInheritedValue(name);
        protected T GetElement<T>(string name) where T : IInheritedConfiguration => this.GetInheritedElement<T>(name);

        bool IInheritedConfiguration.ContainsValueCore(string name) => base.Properties.Contains(name);
        object IInheritedConfiguration.GetValueCore(string name) => base[name];
    }
}