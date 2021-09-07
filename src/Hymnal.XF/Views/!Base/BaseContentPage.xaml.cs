using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Internal implementation only
        /// </summary>
        public void CallOnRendered()
        {
            OnRendered();
        }

        /// <summary>
        /// After rendering
        /// </summary>
        protected virtual void OnRendered()
        { }
    }
}

