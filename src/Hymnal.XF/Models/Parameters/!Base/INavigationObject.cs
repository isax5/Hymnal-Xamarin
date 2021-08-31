namespace Hymnal.XF.Models.Parameters
{
    public interface INavigationObject
    {
        static string Key { get; }
        string GetKey();
    }
}
