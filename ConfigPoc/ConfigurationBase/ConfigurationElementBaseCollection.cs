﻿using System.Collections.Generic;
using System.Configuration;

namespace ConfigPoc
{
    public abstract class ConfigurationElementBaseCollection<T> : ConfigurationElementCollection, IInheritedConfiguration
        where T : ConfigurationElementBase, new()
    {
        public IInheritedConfiguration Parent { get; set; }
        protected override ConfigurationElement CreateNewElement() { return new T(); }
        protected override object GetElementKey(ConfigurationElement element) { return ((T)element).Key; }
        protected string GetValue(string name) { return this.GetInheritedValue(name); }
        public T this[int index] { get { return this.GetElement(index); } }

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

        bool IInheritedConfiguration.TryGetValueCore(string name, out string value)
        {
            value = null;
            if (!base.Properties.Contains(name))
                return false;

            value = base[name].ToString();
            return true;
        }
        U IInheritedConfiguration.GetElementCore<U>(string name) { return (U)base[name]; }
    }
}
