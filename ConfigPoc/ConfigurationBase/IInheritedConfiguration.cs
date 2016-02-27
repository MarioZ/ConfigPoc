namespace ConfigPoc
{
    public interface IInheritedConfiguration
    {
        IInheritedConfiguration Parent { get; set; }
        bool TryGetValueCore(string name, out string value);
        T GetElementCore<T>(string name) where T : IInheritedConfiguration;

        /* CONSIDER:
         * bool TryGetNumberValueCore(string name, string format, out string value);
         * bool TryGetDateValueCore(string name, string format, out string value);
         *
         * $(placeholderName \# "Standard or Custom Numeric Format")
         * $(placeholderName \@ "Standard or Custom Date and Time Format") */
    }
}
