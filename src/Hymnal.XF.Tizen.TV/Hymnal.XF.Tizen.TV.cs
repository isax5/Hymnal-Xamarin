using MvvmCross.Forms.Platforms.Tizen.Views;

namespace Hymnal.XF.Tizen.TV
{
    public class Program : MvxFormsTizenApplication<Setup, Core.App, UI.App>
    {

        public static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
