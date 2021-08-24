using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Foundation;
using MvvmHelpers;
using Prism.Commands;
using Prism.Mvvm;
using UIKit;

namespace Hymnal.XF.iOS.Linker
{
    // This class is never actually executed, but when Xamarin linking is enabled it does ensure types and properties
    // are preserved in the deployed app
    [Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        #region XF

        public void Include(UIButton uiButton)
        {
            uiButton.TouchUpInside += (s, e) =>
                uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
        }

        public void Include(UIBarButtonItem barButton)
        {
            barButton.Clicked += (s, e) =>
                barButton.Title = $"{barButton.Title}";
        }

        public void Include(UITextField textField)
        {
            textField.Text = $"{textField.Text}";
            textField.EditingChanged += (sender, args) => { textField.Text = ""; };
            textField.EditingDidEnd += (sender, args) => { textField.Text = ""; };
        }

        public void Include(UITextView textView)
        {
            textView.Text = $"{textView.Text}";
            textView.TextStorage.DidProcessEditing += (sender, e) => textView.Text = "";
            textView.Changed += (sender, args) => { textView.Text = ""; };
        }

        public void Include(UILabel label)
        {
            label.Text = $"{label.Text}";
            label.AttributedText = new NSAttributedString($"{label.AttributedText.ToString()}");
        }

        public void Include(UIImageView imageView)
        {
            imageView.Image = new UIImage(imageView.Image.CGImage);
        }

        public void Include(UIControl control)
        {
            control.ValueChanged += (sender, args) => { control.ClipsToBounds = true; };
        }

        public void Include(UIDatePicker date)
        {
            date.Date = date.Date.AddSeconds(1);
        }

        public void Include(UISlider slider)
        {
            slider.Value = slider.Value + 1;
        }

        public void Include(UIProgressView progress)
        {
            progress.Progress = progress.Progress + 1;
        }

        public void Include(UISwitch sw)
        {
            sw.On = !sw.On;
        }

        public void Include(UIStepper s)
        {
            s.Value = s.Value + 1;
        }

        public void Include(UIPageControl s)
        {
            s.Pages = s.Pages + 1;
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) =>
            {
                _ = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}";
            };
        }

        public void Include(INotifyPropertyChanged changed)
        {
            changed.PropertyChanged += (sender, e) => { _ = e.PropertyName; };
        }

        #endregion

        #region Prism

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) =>
            {
                if (command.CanExecute(null)) command.Execute(null);
            };
        }

        public void Include(ConsoleColor color)
        {
            Console.Write("");
            Console.WriteLine("");
            _ = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        public void Include(DelegateCommand delegateCommand)
        {
            delegateCommand.CanExecuteChanged += (s, e) =>
            {
                if (delegateCommand.CanExecute()) delegateCommand.Execute();
            };
        }

        public void Include(BindableBase bindableBase)
        {
            bindableBase.PropertyChanged += (s, e) =>
            {
                _ = e.PropertyName;
            };
        }

        #endregion

        #region  MVVMHelpers

        public void Include(ObservableRangeCollection<object> rangeCollection)
        {
            rangeCollection.Clear();
            rangeCollection.ReplaceRange(new List<object>());
            rangeCollection.Remove(new object());
        }

        #endregion
    }
}
