using MvvmCross.Forms.Platforms.Tizen.Views;

namespace Hymnal.Tizen.TV
{
    public class Program : MvxFormsTizenApplication<Setup, Core.App, XF.UI.App>
    {
        //protected override void OnCreate()
        //{
        //    base.OnCreate();
        //    LoadApplication(new App());
        //}

        public static void Main(string[] args)
        {
            var app = new Program();
            //global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
