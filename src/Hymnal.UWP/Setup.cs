using Hymnal.Core.Services;
using Hymnal.SharedNatives.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;

namespace Hymnal.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, UI.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            // Native services register
            Mvx.IoCProvider.RegisterType<IFilesService, FilesService>();
        }
    }
}
