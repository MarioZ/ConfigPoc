using System.Configuration;

namespace ConfigPoc
{
    public abstract class ConfigurationElementBase : ConfigurationElement, IInheritedConfiguration
    {
        public IInheritedConfiguration Parent { get; set; }
        public virtual string Key { get { return null; } }
        protected string GetValue(string name) { return this.GetInheritedValue(name); }
        protected T GetElement<T>(string name) where T : IInheritedConfiguration { return this.GetInheritedElement<T>(name); }

        bool IInheritedConfiguration.ContainsValueCore(string name) { return base.Properties.Contains(name); }
        object IInheritedConfiguration.GetValueCore(string name) { return base[name]; }
    }
}
