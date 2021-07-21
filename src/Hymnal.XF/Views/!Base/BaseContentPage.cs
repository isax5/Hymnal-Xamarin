using Xamarin.Forms;

namespace Hymnal.XF.Views
{
    public class BaseContentPage<TViewModel> : ContentPage where TViewModel: class
    {
        public TViewModel ViewModel
        {
            get => BindingContext as TViewModel;
            set => BindingContext = value;
        }

        /*
        private bool playerAlwaysVisible;
        protected bool PlayerVisible
        {
            get => playerAlwaysVisible;
            set
            {
                if (value == playerAlwaysVisible)
                    return;

                if (value)
                {
                    ToolbarItems.Add(PlaySomethingToolbarItem);
                    ToolbarItems.Add(OpenPlayerToolbarItem);
                }
                else
                {
                    ToolbarItems.Remove(PlaySomethingToolbarItem);
                    ToolbarItems.Remove(OpenPlayerToolbarItem);
                }
            }
        }

        protected ToolbarItem PlaySomethingToolbarItem = new ToolbarItem
        {
            Text = "Play",
            Order = ToolbarItemOrder.Secondary
        };
        protected ToolbarItem OpenPlayerToolbarItem = new ToolbarItem
        {
            Text = "Open player",
            Order = ToolbarItemOrder.Secondary
        };



        public BaseContentPage(bool playerVisible = false)
        {
            PlayerVisible = playerVisible;
        }
        */
    }
}
