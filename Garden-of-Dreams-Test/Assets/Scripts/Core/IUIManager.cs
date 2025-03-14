namespace Menedment.Core
{
    public interface IUIManager
    {
        bool IsRemoving { get; }
        void ResetRemoveMode();
    }
}