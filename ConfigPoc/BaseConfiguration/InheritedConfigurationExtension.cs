using System;
using System.Text.RegularExpressions;

namespace ConfigPoc
{
    public static class InheritedConfigurationExtension
    {
        private const string PlaceholderPattern =
            @"\$\((?<Name>([\w]+))( \\(?<FormatType>@|#) (?<Format>[^)]+))?\)";
        private static readonly Regex PlaceholderRegex =
            new Regex(PlaceholderPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static T GetInheritedElement<T>(this IInheritedConfiguration element, string name) where T : IInheritedConfiguration
        {
            T childElement = (T)element.GetValueCore(name);

            if (childElement.Parent == null)
                childElement.Parent = element;

            return childElement;
        }

        public static string GetInheritedValue(this IInheritedConfiguration element, string name)
        {
            if (!element.ContainsValueCore(name))
                return null;

            string value = element.GetValueCore(name).ToString();
            foreach (Match match in PlaceholderRegex.Matches(value))
            {
                var placeholder = new PlaceholderInfo(match);
                if (placeholder.Name == name)
                    continue;

                string inheritedValue = null;
                IInheritedConfiguration parentElement = element;
                do
                {
                    inheritedValue = parentElement.GetInheritedValue(placeholder.Name);
                    parentElement = parentElement.Parent;
                }
                while (string.IsNullOrEmpty(inheritedValue) && parentElement != null);

                inheritedValue = FormatInheritedValue(inheritedValue, placeholder);
                value = value.Replace(placeholder.Content, inheritedValue);
            }

            return value;
        }

        private static string FormatInheritedValue(string inheritedValue, PlaceholderInfo placeholder)
        {
            switch (placeholder.FormatType)
            {
                case FormatType.Numeric:
                    double numberValue;
                    if (double.TryParse(inheritedValue, out numberValue))
                        return numberValue.ToString(placeholder.Format);
                    break;
                case FormatType.DateAndTime:
                    DateTime dateValue;
                    if (DateTime.TryParse(inheritedValue, out dateValue))
                        return dateValue.ToString(placeholder.Format);
                    break;
                default:
                    return inheritedValue;
            }

            return null;
        }

        private enum FormatType
        {
            Numeric = '#',
            DateAndTime = '@'
        }

        private class PlaceholderInfo
        {
            private readonly string content, name, format;
            private readonly FormatType formatType;

            public PlaceholderInfo(Match match)
            {
                this.content = match.Value;
                this.name = match.Groups[nameof(this.Name)].Value;
                this.format = match.Groups[nameof(this.Format)].Value;

                string type = match.Groups[nameof(this.FormatType)].Value;
                if (!string.IsNullOrEmpty(type))
                    this.formatType = (FormatType)type[0];
            }

            public string Content => this.content;
            public string Name => this.name;
            public string Format => this.format;
            public FormatType FormatType => this.formatType;
        }
    }
}