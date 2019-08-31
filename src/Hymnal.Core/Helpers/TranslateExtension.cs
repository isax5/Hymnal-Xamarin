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

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            CultureInfo ci = Constants.CurrentCultureInfo;

            var translation = LanguajesResourcesManager.Value.GetString(Text, ci);

            if (translation == null)
            {

#if DEBUG
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }


    /// <summary>
    /// This class exposes the translations to use in C# Code
    /// </summary>
    public static class Languages
    {
        public static string ChooseYourHymnal => AppResources.ChooseYourHymnal;
        public static string Cancel => AppResources.Cancel;
        public static string WeHadAProblem => AppResources.WeHadAProblem;
        public static string Ok => AppResources.Ok;
        public static string NoInternetConnection => AppResources.NoInternetConnection;
    }
}
