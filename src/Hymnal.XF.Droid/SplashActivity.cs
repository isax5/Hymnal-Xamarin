using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace Hymnal.XF.Droid
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        Label = "@string/app_name",
        Theme = "@style/AppTheme.Splash",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher_round")]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
