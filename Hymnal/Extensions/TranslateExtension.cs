using System.Globalization;
using System.Resources;

namespace Hymnal.Extensions;
[ContentProperty("Text")]
public sealed class TranslateExtension : IMarkupExtension
{
    public string Text { get; set; }

    private static ResourceManager languageResources;

    public static void Configure(ResourceManager resourceManager)
        => languageResources = resourceManager;

    public static string GetTranslation(string text, CultureInfo ci = null)
        => languageResources.GetString(text, ci) ?? $"#Translation: {text}";

    public object ProvideValue(IServiceProvider serviceProvider)
        => GetTranslation(Text);
}
