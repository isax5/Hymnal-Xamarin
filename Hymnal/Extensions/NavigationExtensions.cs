namespace Hymnal.Extensions;

public static class NavigationExtensions
{
    public static Dictionary<string, object> AsParameter(this object navigationParameter)
    {
        return new Dictionary<string, object>()
        {
            [nameof(BaseViewModelParameter<object>.Parameter)] = navigationParameter,
        };
    }
}
