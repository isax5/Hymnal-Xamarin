using System;
using System.Globalization;
using Hymnal.XF.Resources.Languages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Extensions
{
    [ContentProperty("Text")]
    public sealed class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public static string GetTranslation(string text, CultureInfo ci = null)
            => LanguageResources.ResourceManager.GetString(text, ci) ?? $"#Translation: {text}";

        public object ProvideValue(IServiceProvider serviceProvider)
            => GetTranslation(Text);
    }
}
