namespace Hymnal.XF.Models.Parameters
{
    public abstract class NavigationParameter : INavigationObject
    {
        public static string Key => typeof(NavigationParameter).Name;
        public string GetKey() => Key;
    }
}
