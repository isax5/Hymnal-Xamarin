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
            Constants.CurrentCultureInfo = Mvx.IoCProvider.Resolve<IMultilingualService>().DeviceCultureInfo;

            AppResources.Culture = Constants.CurrentCultureInfo;

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }
    }
}
