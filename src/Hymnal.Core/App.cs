using Hymnal.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Hymnal.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }
    }
}
