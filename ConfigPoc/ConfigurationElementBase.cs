using System.Configuration;
using System.Runtime.CompilerServices;

namespace ConfigPoc
{
    public abstract class ConfigurationElementBase : ConfigurationElement
    {
        protected string GetValue([CallerMemberName]string name = "") => this.GetResolvedValue(name);
        protected T GetElement<T>([CallerMemberName]string name = "") where T : ConfigurationElement => this.GetChildElement<T>(name);
    }
}
