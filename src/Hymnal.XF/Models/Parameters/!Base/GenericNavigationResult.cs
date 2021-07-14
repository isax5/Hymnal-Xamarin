namespace Hymnal.XF.Models.Parameters
{
    public class GenericNavigationResult<T> : NavigationResult
    {
        public T Value { get; private set; }

        public GenericNavigationResult(T value)
        {
            Value = Value;
        }
    }
}
