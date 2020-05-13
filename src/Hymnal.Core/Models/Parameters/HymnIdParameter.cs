namespace Hymnal.Core.Models.Parameter
{
    /// <summary>
    /// Useful for parameters in transitions
    /// </summary>
    public class HymnIdParameter
    {
        public int Number { get; set; }
        public bool SaveInRecords { get; set; } = true;
        public HymnalLanguage HymnalLanguage { get; set; }
    }
}
