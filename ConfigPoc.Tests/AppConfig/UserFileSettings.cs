using System.Configuration;

namespace ConfigPoc.Tests
{
    internal sealed class UserFileSection : ConfigurationSectionBase
    {
        [ConfigurationProperty(nameof(Root))]
        public string Root => base.GetValue();

        [ConfigurationProperty(nameof(Files)), ConfigurationCollection(typeof(FileCollection), AddItemName = "File")]
        public FileCollection Files => base.GetElement<FileCollection>();
    }

    internal sealed class FileCollection : ConfigurationElementCollectionBase<FileElement>
    {
        [ConfigurationProperty(nameof(Documents))]
        public string Documents => base.GetValue();

        [ConfigurationProperty(nameof(Videos))]
        public string Videos => base.GetValue();
    }

    internal sealed class FileElement : ConfigurationElementBase
    {
        [ConfigurationProperty(nameof(Path), IsKey = true)]
        public string Path => base.GetValue();
    }
}
