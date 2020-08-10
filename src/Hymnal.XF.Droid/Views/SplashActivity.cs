using System.Threading.Tasks;
using Android.App;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace Hymnal.XF.Droid.Views
{
    //[IntentFilter(new[] { Intent.ActionView },
    //    Categories = new[] { Intent.CategoryDefault },
    //    DataScheme = Core.Constants.AppLink.Scheme,
    //    DataHost = Core.Constants.AppLink.Host,
    //    AutoVerify = true)]
    [Activity(
       NoHistory = true,
       MainLauncher = true,
       Label = "@string/app_name",
       Theme = "@style/AppTheme.Splash",
       Icon = "@mipmap/ic_launcher",
       RoundIcon = "@mipmap/ic_launcher_round")]
    public class SplashActivity : MvxFormsSplashScreenAppCompatActivity<Setup, Core.App, UI.App>
    {
        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }
    }
}
