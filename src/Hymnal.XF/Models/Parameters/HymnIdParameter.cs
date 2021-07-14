namespace Hymnal.XF.Models.Parameters
{
    /// <summary>
    /// Useful for parameters in transitions
    /// </summary>
    public class HymnIdParameter : NavigationParameter
    {
        public int Number { get; set; }
        public bool SaveInRecords { get; set; } = true;
        public HymnalLanguage HymnalLanguage { get; set; }
    }
}
