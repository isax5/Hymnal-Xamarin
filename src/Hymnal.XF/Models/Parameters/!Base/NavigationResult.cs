namespace Hymnal.XF.Models.Parameters
{
    public class NavigationResult : INavigationObject
    {
        public static string Key => typeof(NavigationResult).Name;

        public string GetKey() => Key;
    }
}
