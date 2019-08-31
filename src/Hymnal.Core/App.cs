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
            SetUp();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }

        private void SetUp()
        {
            IMultilingualService multilingualService = Mvx.IoCProvider.Resolve<IMultilingualService>();
            IPreferencesService preferencesService = Mvx.IoCProvider.Resolve<IPreferencesService>();
            IAppInformationService appInformationService = Mvx.IoCProvider.Resolve<IAppInformationService>();

            // Configurating language of the device
            Constants.CurrentCultureInfo = multilingualService.DeviceCultureInfo;
            AppResources.Culture = Constants.CurrentCultureInfo;

            // Configurating language of the hymnals
            if (preferencesService.ConfiguratedHymnalLanguage == null)
            {
                List<HymnalLanguage> lngs = Constants.HymnsLanguages.FindAll(l => l.TwoLetterISOLanguageName == Constants.CurrentCultureInfo.TwoLetterISOLanguageName);

                if (lngs.Count == 0)
                {
                    preferencesService.ConfiguratedHymnalLanguage = Constants.HymnsLanguages.First();
                }
                else
                {
                    preferencesService.ConfiguratedHymnalLanguage = lngs.First();
                }
            }

            // Configurate First Time opening this version
            if (preferencesService.LastVersionOpened != appInformationService.VersionString)
            {
                preferencesService.LastVersionOpened = appInformationService.VersionString;

                // Do somethink

            }
        }
    }
}
