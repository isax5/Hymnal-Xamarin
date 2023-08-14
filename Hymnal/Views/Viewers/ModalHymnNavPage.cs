namespace Hymnal.Views;

public sealed class ModalHymnNavPage : ModalNavPage
{
    public ModalHymnNavPage(HymnPage hymnPage) : base(hymnPage)
    {
        var page = hymnPage;
    }
}
