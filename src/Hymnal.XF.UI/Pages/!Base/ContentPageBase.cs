using System;
using Hymnal.Core.ViewModels;
using MediaManager;
using MediaManager.Playback;
using MediaManager.Player;
using MvvmCross;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Hymnal.XF.UI.Pages
{
    public class BaseContentPage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        private readonly bool alwaysPlayerVisible;
        protected event EventHandler PlaySomething;


        public BaseContentPage(bool alwaysPlayerVisible = false)
        {
            this.alwaysPlayerVisible = alwaysPlayerVisible;


            IMediaManager mediaManager = Mvx.IoCProvider.Resolve<IMediaManager>();
            mediaManager.StateChanged += MediaManager_StateChanged;

            if (alwaysPlayerVisible)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Play Button",
                    Order = ToolbarItemOrder.Secondary,
                    Command = new Command(() => PlaySomething?.Invoke(this, new EventArgs()))
                });
            }
        }

        private void MediaManager_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch (e.State)
            {
                case MediaPlayerState.Loading:
                case MediaPlayerState.Buffering:
                case MediaPlayerState.Playing:
                    break;

                case MediaPlayerState.Failed:
                case MediaPlayerState.Stopped:
                case MediaPlayerState.Paused:
                    break;

                default:
                    break;
            }
        }

    }
}