namespace Hymnal.XF.Views
{
    public interface IModalPage
    {
        string CloseButtonText { get; }
        void PopModal();
    }
}
