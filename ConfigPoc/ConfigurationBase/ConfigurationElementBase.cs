using System.Configuration;

namespace ConfigPoc
{
    public abstract class ConfigurationElementBase : ConfigurationElement, IInheritedConfiguration
    {
        public IInheritedConfiguration Parent { get; set; }
        public virtual string Key { get { return null; } }
        protected string GetValue(string name) { return this.GetInheritedValue(name); }
        protected T GetElement<T>(string name) where T : IInheritedConfiguration { return this.GetInheritedElement<T>(name); }

        bool IInheritedConfiguration.TryGetValueCore(string name, out string value)
        {
            value = null;
            if (!base.Properties.Contains(name))
                return false;

            value = base[name].ToString();
            return true;
        }
        T IInheritedConfiguration.GetElementCore<T>(string name) { return (T)base[name]; }
    }
}
