using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Hymnal.XF.Extensions.i18n
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static CultureInfo currentCultureInfo;
        public static CultureInfo CurrentCultureInfo
        {
            get => currentCultureInfo;
            set
            {
                LanguageResources.Culture = value;
                currentCultureInfo = value;
            }
        }
        private const string ResourceId = "Hymnal.XF.Resources.Languages.LanguageResources";
        private static readonly Lazy<ResourceManager> LanguajesResourcesManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public static string GetTranslation(string text, CultureInfo ci = null)
        {
            if (ci == null)
                ci = CurrentCultureInfo;

            var translation = LanguajesResourcesManager.Value.GetString(text, ci);

            if (translation == null)
            {
                return $"#Translation: {text}";
            }

            return translation;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetTranslation(Text);
        }
    }
}
