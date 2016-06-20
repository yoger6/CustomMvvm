namespace CustomMvvm.Utilities
{
    public interface ISettingsProvider
    {
        object this[string name] { get; set; }
        void Save();
    }
}
