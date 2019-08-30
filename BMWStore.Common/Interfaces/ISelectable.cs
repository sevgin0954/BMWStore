namespace BMWStore.Common.Interfaces
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }

        string Value { get; set; }
    }
}