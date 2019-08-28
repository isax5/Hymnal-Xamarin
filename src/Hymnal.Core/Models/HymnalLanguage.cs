namespace Hymnal.Core.Models
{
    public class HymnalLanguage
    {
        /// <summary>
        /// Identifier for comparative Equals
        /// </summary>
        public string Id { get; set; }
        public string TwoLetterISOLanguageName { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string HymnsFileName { get; set; }
        public string ThematicHymnsFileName { get; set; }
        public bool SupportThematicList => !string.IsNullOrWhiteSpace(ThematicHymnsFileName);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var hl = obj as HymnalLanguage;

            if (Id == hl.Id)
            {
                return true;
            }

            return false;
        }
    }
}
