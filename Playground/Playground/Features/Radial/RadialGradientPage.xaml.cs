using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Playground.Features.Radial
{
    public partial class RadialGradientPage : ContentPage
    {
        private SKSizeI _size;
        private SKPoint _prev;
        private long _touchId;

        public RadialGradientPage()
        {
            InitializeComponent();
        }

        private void GradientView_Touch(object sender, SKTouchEventArgs e)
        {
            var x = e.Location.X / _size.Width;
            var y = e.Location.Y / _size.Height;

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    _touchId = e.Id;
                    _prev = new SKPoint(x, y);
                    break;

                case SKTouchAction.Moved:
                    if (_touchId == e.Id)
                    {
                        var deltaX = x - _prev.X;
                        var deltaY = y - _prev.Y;

                        //var vm = (RadialGradientViewModel)BindingContext;
                        //vm.CenterX += deltaX;
                        //vm.CenterY += deltaY;

                        _prev = new SKPoint(x, y);
                    }
                    break;

                case SKTouchAction.Released:
                case SKTouchAction.Cancelled:
                    _touchId = -1;
                    break;
            }

            e.Handled = true;
        }

        private void GradientView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            _size = e.Info.Size;
        }
    }
}