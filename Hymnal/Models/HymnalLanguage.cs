namespace Hymnal.Models;

#pragma warning disable CS0659 // El tipo reemplaza a Object.Equals(object o), pero no reemplaza a Object.GetHashCode()
public sealed class HymnalLanguage
#pragma warning restore CS0659 // El tipo reemplaza a Object.Equals(object o), pero no reemplaza a Object.GetHashCode()
{
    /// <summary>
    /// Identifier for comparative Equals
    /// </summary>
    public string Id { get; set; }
    public string TwoLetterIsoLanguageName { get; set; }
    public string Name { get; set; }
    public string Detail { get; set; }
    public int Year { get; set; }
    public string HymnsFileName { get; set; }

    public string ThematicHymnsFileName { get; set; }
    public bool SupportThematicList => !string.IsNullOrWhiteSpace(ThematicHymnsFileName);

    public string HymnsSheetsFileName { get; set; }
    public bool SupportSheets => !string.IsNullOrWhiteSpace(HymnsSheetsFileName);

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        var hl = obj as HymnalLanguage;

        return Id == hl.Id;
    }

    public static HymnalLanguage GetHymnalLanguageWithId(string id)
    {
        return InfoConstants.HymnsLanguages.FirstOrDefault(l => l.Id.Equals(id));
    }
}
