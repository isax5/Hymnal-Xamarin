using System;
using Xamarin.Forms;

// This code is taken from https://github.com/xamarin/Xamarin.Forms/issues/8462
// and modified to scroll through a StackLayout with contents possibly larger than
// will fit on screen. It expects a single StackLayout child.

namespace Hymnal.XF.Views
{
    public class PinchToZoomContainer : ContentView
    {
        private double _startScale, _currentScale;
        private double _startX, _startY;
        private double _xOffset, _yOffset;
        private double _yPrePinchOffset;
        // total height of the child stacklayout, possibly longer than the PinchToZoomContainer
        private double _totalHeight;

        public double MinScale { get; set; } = 1;
        public double MaxScale { get; set; } = 4;

        public PinchToZoomContainer()
        {
            var tap = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            tap.Tapped += OnTapped;
            GestureRecognizers.Add(tap);

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var pan = new PanGestureRecognizer();
            pan.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(pan);
        }

        private double Clamp(double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }

        private void GetTotalHeight()
        {
            var layout = (StackLayout)Content;
            var imageHeight = 0.0;

            foreach (View i in layout.Children)
            {
                imageHeight += i.Height;
            }
            _totalHeight = imageHeight;
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            RestoreScaleValues();
            Content.AnchorX = 0.5;
            Content.AnchorY = 0.5;

            base.OnSizeAllocated(width, height);
        }

        private void RestoreScaleValues()
        {
            Content.ScaleTo(MinScale, 250, Easing.CubicInOut);
            Content.TranslateTo(0, 0, 250, Easing.CubicInOut);

            _currentScale = MinScale;
            _xOffset = Content.TranslationX = 0;
            _yOffset = Content.TranslationY = 0;
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (Content.Scale > MinScale)
            {
                RestoreScaleValues();
            }
            else
            {
                //todo: Add tap position somehow
                StartScaling();
                ExecuteScaling(MaxScale, .5, .5);
                EndGesture();
            }
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Started:
                    GetTotalHeight();
                    StartScaling();
                    break;

                case GestureStatus.Running:
                    ExecuteScaling(e.Scale, e.ScaleOrigin.X, e.ScaleOrigin.Y);
                    break;

                case GestureStatus.Completed:
                    EndGesture();
                    break;
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    GetTotalHeight();
                    _startX = e.TotalX;
                    _startY = e.TotalY;

                    Content.AnchorX = 0;
                    Content.AnchorY = 0;

                    break;

                case GestureStatus.Running:
                    var maxTranslationX = Content.Scale * Content.Width - Content.Width;
                    Content.TranslationX = Math.Min(0, Math.Max(-maxTranslationX, _xOffset + e.TotalX - _startX));

                    var maxTranslationY = Math.Max(0, Content.Scale * _totalHeight - Content.Height);
                    Content.TranslationY = Math.Min(0, Math.Max(-maxTranslationY, _yOffset + e.TotalY - _startY));

                    break;

                case GestureStatus.Completed:
                    EndGesture();
                    break;
            }
        }

        private void StartScaling()
        {
            _startScale = Content.Scale;
            _yPrePinchOffset = _yOffset;

            Content.AnchorX = 0;
            Content.AnchorY = 0;
        }

        private void ExecuteScaling(double scale, double x, double y)
        {
            _currentScale += (scale - 1) * _startScale;
            _currentScale = Math.Max(MinScale, _currentScale);
            _currentScale = Math.Min(MaxScale, _currentScale);

            var deltaX = (Content.X + _xOffset) / Width;
            var deltaWidth = Width / (Content.Width * _startScale);
            var originX = (x - deltaX) * deltaWidth;

            var deltaY = (Content.Y + _yOffset) / Height;
            var deltaHeight = Height / (Content.Height * _startScale);
            var originY = (y - deltaY) * deltaHeight;

            var targetX = _xOffset - (originX * Content.Width) * (_currentScale - _startScale);
            var targetY = _yOffset - (originY * Content.Height) * (_currentScale - _startScale);
            
            var maxTranslationY = Math.Max(0, _currentScale * Math.Max(_totalHeight, Content.Height) - Content.Height);
            targetY = Math.Max(-maxTranslationY, targetY);

            Content.TranslationX = Clamp(targetX, -Content.Width * (_currentScale - 1), 0);
            Content.TranslationY = Clamp(targetY, -_totalHeight * (_currentScale - 1) + _yPrePinchOffset, 0);

            Content.Scale = _currentScale;
        }

        private void EndGesture()
        {
            _xOffset = Content.TranslationX;
            _yOffset = Content.TranslationY;
        }
    }
}
