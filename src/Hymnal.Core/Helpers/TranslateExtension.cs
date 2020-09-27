using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Hymnal.Core.Resources;

namespace Hymnal.Core.Helpers
{
    public class TranslateExtension
    {
        private const string ResourceId = "Hymnal.Core.Resources.AppResources";
        private static readonly Lazy<ResourceManager> LanguajesResourcesManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public static string GetTranslation(string text, CultureInfo ci = null)
        {
            if (ci == null)
                ci = Constants.CurrentCultureInfo;

            var translation = LanguajesResourcesManager.Value.GetString(text, ci);

            if (translation == null)
            {
                return $"Translation error: {text}";
            }

            return translation;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetTranslation(Text);
        }
    }


    /// <summary>
    /// This class exposes the translations to use in C# Code
    /// </summary>
    public static class Languages
    {
        public static string ChooseYourHymnal => AppResources.VersionsAndLanguages;
        public static string Cancel => AppResources.Cancel;
        public static string WeHadAProblem => AppResources.WeHadAProblem;
        public static string Ok => AppResources.Ok;
        public static string NoInternetConnection => AppResources.NoInternetConnection;
        public static string InstrumentalOrSung => AppResources.InstrumentalOrSung;
        public static string Instrumental => AppResources.Instrumental;
        public static string Sung => AppResources.Sung;
    }
}
