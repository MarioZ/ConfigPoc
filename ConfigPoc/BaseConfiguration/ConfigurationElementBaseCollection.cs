using System.Collections.Generic;
using System.Configuration;

namespace ConfigPoc
{
    public abstract class ConfigurationElementBaseCollection<T> : ConfigurationElementCollection, IInheritedConfiguration
        where T : ConfigurationElementBase, new()
    {
        public IInheritedConfiguration Parent { get; set; }
        protected override ConfigurationElement CreateNewElement() => new T();
        protected override object GetElementKey(ConfigurationElement element) => ((T)element).Key;
        protected string GetValue(string name) => this.GetInheritedValue(name);
        public T this[int index] => this.GetElement(index);

        public new IEnumerator<T> GetEnumerator()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
                yield return this.GetElement(i);
        }
        private T GetElement(int index)
        {
            var element = (T)base.BaseGet(index);

            if (element.Parent == null)
                element.Parent = this;

            return element;
        }

        bool IInheritedConfiguration.ContainsValueCore(string name) => base.Properties.Contains(name);
        object IInheritedConfiguration.GetValueCore(string name) => base[name];
    }
}