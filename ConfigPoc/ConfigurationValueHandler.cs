using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConfigPoc
{
    internal static class ConfigurationValueHandler
    {
        private static readonly NameValueCollection Globals = ConfigurationManager.AppSettings;

        private static readonly string PlaceholderPattern = string.Format(@"\((?<{0}>([\w]+))( \\(?<{1}>@|#) (?<{2}>[^)]+))?\)",
            nameof(PlaceholderInfo.Name),
            nameof(PlaceholderInfo.FormatType),
            nameof(PlaceholderInfo.Format));

        private static readonly Regex GlobalPlaceholderRegex = new Regex("@" + PlaceholderPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex ParentPlaceholderRegex = new Regex("\\$" + PlaceholderPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static T GetChildElement<T>(this ConfigurationElement element, string name) where T : ConfigurationElement
        {
            if (TryGetValue(element, name, out T childElement))
            {
                childElement.SetParent(element);
                return childElement;
            }

            throw new InvalidOperationException();
        }

        public static string GetResolvedValue(this ConfigurationElement element, string name)
        {
            if (TryGetValue(element, name, out string value))
            {
                value = ReplaceParentPlaceholders(value, element, name);
                value = ReplaceGlobalPlaceholders(value);
                return value;
            }

            return null;
        }

        private static bool TryGetValue<T>(ConfigurationElement element, string name, out T value)
        {
            PropertyInformationCollection properties = element.ElementInformation.Properties;
            PropertyInformation property = properties.Cast<PropertyInformation>().FirstOrDefault(p => p.Name == name);

            bool hasProperty = property != null;
            value = hasProperty ? (T)property.Value : default(T);
            return hasProperty;
        }
        
        private static string ReplaceParentPlaceholders(string value, ConfigurationElement element, string name)
        {
            foreach (Match match in ParentPlaceholderRegex.Matches(value))
            {
                PlaceholderInfo placeholder = new PlaceholderInfo(match);
                string placeholderName = placeholder.Name, currentValue = null;
                ConfigurationElement currentElement = placeholderName == name ? element.GetParent() : element;

                do
                {
                    currentValue = currentElement.GetResolvedValue(placeholderName);
                    currentElement = currentElement.GetParent();
                }
                while (string.IsNullOrEmpty(currentValue) && currentElement != null);

                value = ReplacePlaceholder(value, placeholder, currentValue);
            }

            return value;
        }

        private static string ReplaceGlobalPlaceholders(string value)
        {
            foreach (Match match in GlobalPlaceholderRegex.Matches(value))
            {
                PlaceholderInfo placeholder = new PlaceholderInfo(match);
                value = ReplacePlaceholder(value, placeholder, Globals[placeholder.Name]);
            }

            return value;
        }

        private static string ReplacePlaceholder(string value, PlaceholderInfo placeholder, string placeholderValue)
        {
            switch (placeholder.FormatType)
            {
                case FormatType.Number:
                    if (double.TryParse(placeholderValue, out double numberValue))
                        placeholderValue = numberValue.ToString(placeholder.Format);
                    break;
                case FormatType.Date:
                    if (DateTime.TryParse(placeholderValue, out DateTime dateValue))
                        placeholderValue = dateValue.ToString(placeholder.Format);
                    break;
            }

            return value.Replace(placeholder.Content, placeholderValue);
        }

        private enum FormatType { Default = 0, Number = '#', Date = '@' }

        private class PlaceholderInfo
        {
            public PlaceholderInfo(Match match)
            {
                this.Content = match.Value;
                this.Name = match.Groups[nameof(Name)].Value;

                string type = match.Groups[nameof(FormatType)].Value;
                if (!string.IsNullOrEmpty(type))
                {
                    this.FormatType = (FormatType)type[0];
                    this.Format = match.Groups[nameof(Format)].Value;
                }
            }

            public string Content { get; }
            public string Name { get; }
            public string Format { get; }
            public FormatType FormatType { get; }
        }
    }
}
