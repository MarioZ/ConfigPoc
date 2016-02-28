namespace ConfigPoc
{
    public interface IInheritedConfiguration
    {
        IInheritedConfiguration Parent { get; set; }
        bool ContainsValueCore(string name);
        object GetValueCore(string name);
    }
}
