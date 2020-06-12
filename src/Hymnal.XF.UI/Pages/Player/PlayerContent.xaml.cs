using System;
using System.Collections.Generic;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class PlayerContent : MvxContentPage<PlayerViewModel>
    {
        public PlayerContent()
        {
            InitializeComponent();


#if !__IOS__
            ToolbarItems.Remove(CloseToolbar);
#endif
        }
    }
}
