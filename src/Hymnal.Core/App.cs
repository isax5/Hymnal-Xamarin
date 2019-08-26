using System.Collections.Generic;
using System.Linq;
using Hymnal.Core.Models;
using Hymnal.Core.Resources;
using Hymnal.Core.Services;
using Hymnal.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Hymnal.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            ConfigurateLocalization();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }

        private void ConfigurateLocalization()
        {
            // Configurating language of the device
            Constants.CurrentCultureInfo = Mvx.IoCProvider.Resolve<IMultilingualService>().DeviceCultureInfo;
            AppResources.Culture = Constants.CurrentCultureInfo;


            // Configurating language of the hymnals
            IPreferencesService settingsService = Mvx.IoCProvider.Resolve<IPreferencesService>();

            if (settingsService.ConfiguratedHymnalLanguage == null)
            {
                List<HymnalLanguage> lngs = Constants.HymnsLanguages.FindAll(l => l.TwoLetterISOLanguageName == Constants.CurrentCultureInfo.TwoLetterISOLanguageName);

                if (lngs.Count == 0)
                {
                    settingsService.ConfiguratedHymnalLanguage = Constants.HymnsLanguages.First();
                }
                else
                {
                    settingsService.ConfiguratedHymnalLanguage = lngs.First();
                }
            }
        }
    }
}
