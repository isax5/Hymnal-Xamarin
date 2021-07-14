namespace Hymnal.XF.Models.Parameters
{
    public class GenericNavigationParameter<T> : NavigationParameter
    {
        public T Value { get; private set; }

        public GenericNavigationParameter(T value)
        {
            Value = value;
        }
    }
}
