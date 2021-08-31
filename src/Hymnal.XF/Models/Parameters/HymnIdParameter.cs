namespace Hymnal.XF.Models.Parameters
{
    /// <summary>
    /// Useful for parameters in transitions
    /// </summary>
    public sealed class HymnIdParameter : NavigationParameter
    {
        public int Number { get; set; }
        public bool SaveInRecords { get; set; } = true;
        public HymnalLanguage HymnalLanguage { get; set; }
    }
}
