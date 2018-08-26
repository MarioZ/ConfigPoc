using System.Configuration;

namespace ConfigPoc.Tests
{
    internal sealed class UserProfileSection : ConfigurationSectionBase
    {
        [ConfigurationProperty(nameof(Name))]
        public string Name => base.GetValue();

        [ConfigurationProperty(nameof(SocialMedias)), ConfigurationCollection(typeof(SocialMediaCollection), AddItemName = "SocialMedia")]
        public SocialMediaCollection SocialMedias => base.GetElement<SocialMediaCollection>();

        [ConfigurationProperty(nameof(Blog))]
        public BlogElement Blog => base.GetElement<BlogElement>();
    }

    internal sealed class SocialMediaCollection : ConfigurationElementCollectionBase<SocialMediaElement> { }

    internal sealed class SocialMediaElement : ConfigurationElementBase
    {
        [ConfigurationProperty(nameof(Location), IsKey = true)]
        public string Location => base.GetValue();
    }

    internal sealed class BlogElement : ConfigurationElementBase
    {
        [ConfigurationProperty(nameof(Location))]
        public string Location => base.GetValue();

        [ConfigurationProperty(nameof(Posts))]
        public string Posts => base.GetValue();

        [ConfigurationProperty(nameof(PostsMessage))]
        public string PostsMessage => base.GetValue();
    }
}
