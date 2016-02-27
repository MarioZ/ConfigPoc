using System.Text.RegularExpressions;

namespace ConfigPoc
{
    public static class InheritedConfigurationExtension
    {
        private static readonly Regex inheritPlaceholderRegex = new Regex(@"\$\([\w.]+\)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static string GetInheritedValue(this IInheritedConfiguration element, string name)
        {
            string value;
            if (!element.TryGetValueCore(name, out value))
                return null;

            foreach (Match match in inheritPlaceholderRegex.Matches(value))
            {
                IInheritedConfiguration parentElement = element;
                string placeholder = match.Value;
                string placeholderName = placeholder.Substring(2, placeholder.Length - 3);
                string placeholderValue = null;

                if (placeholderName != name)
                {
                    do
                    {
                        placeholderValue = parentElement.GetInheritedValue(placeholderName);
                        parentElement = parentElement.Parent;
                    }
                    while (string.IsNullOrEmpty(placeholderValue) && parentElement != null);
                }

                value = value.Replace(placeholder, placeholderValue);
            }

            return value;
        }

        public static T GetInheritedElement<T>(this IInheritedConfiguration element, string name) where T : IInheritedConfiguration
        {
            T childElement = element.GetElementCore<T>(name);

            if (childElement.Parent == null)
                childElement.Parent = element;

            return childElement;
        }
    }
}
