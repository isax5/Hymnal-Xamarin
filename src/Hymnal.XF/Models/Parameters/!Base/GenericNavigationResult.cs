namespace Hymnal.XF.Models.Parameters
{
    public abstract class GenericNavigationResult<T> : NavigationResult
    {
        public T Value { get; private set; }

        public GenericNavigationResult(T value)
        {
            Value = Value;
        }
    }
}
