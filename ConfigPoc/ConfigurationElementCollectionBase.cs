using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConfigPoc
{
    public abstract class ConfigurationElementCollectionBase<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElementBase, new()
    {
        protected string GetValue([CallerMemberName]string name = "") => this.GetResolvedValue(name);

        protected override ConfigurationElement CreateNewElement()
        {
            T element = new T();
            element.SetParent(this);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element) =>
            element.ElementInformation.Properties.Cast<PropertyInformation>().First(property => property.IsKey).Value;

        public new IEnumerator<T> GetEnumerator()
        {
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
                yield return (T)enumerator.Current;
        }
    }
}
