namespace Hymnal.XF.Models.Parameters
{
    public sealed class GenericNavigationParameter<T> : NavigationParameter
    {
        public T Value { get; private set; }

        public GenericNavigationParameter(T value)
        {
            Value = value;
        }
    }
}
