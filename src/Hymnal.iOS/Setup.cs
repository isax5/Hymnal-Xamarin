using MvvmCross.Forms.Platforms.Ios.Core;
using Xamarin.Forms;

namespace Hymnal.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, UI.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        public override void InitializePrimary()
        {
            base.InitializePrimary();
            FormsMaterial.Init();
        }
    }
}
