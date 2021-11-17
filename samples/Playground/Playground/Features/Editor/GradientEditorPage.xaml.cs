using MagicGradients;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Threading.Tasks;
using Playground.Extensions;
using Xamarin.Forms;
using VM = Playground.Features.Editor.GradientEditorViewModel;

namespace Playground.Features.Editor
{
    public partial class GradientEditorPage : ContentPage
    {
        private readonly string MaskPath = $"{nameof(VM.Mask)}.{nameof(VM.Mask.Collection)}";
        private readonly string UseLegacyShaderPath = $"{nameof(VM.Linear)}.{nameof(VM.Linear.UseLegacyShader)}";

        private SKSizeI _size;
        private SKPoint _prev;
        private long _touchId;

        public GradientEditorPage()
        {
            InitializeComponent();
            AddSoftwareView();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = this.GetSafeAreaInsets();
            Editor.Margin = new Thickness(0, 0, 0, safeInsets.Bottom * -1);
        }

        private void SKCanvasView_OnTouch(object sender, SKTouchEventArgs e)
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

                        var vm = (GradientEditorViewModel)BindingContext;
                        vm.Radial.CenterX += deltaX;
                        vm.Radial.CenterY += deltaY;

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

        private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            _size = e.Info.Size;
        }

        private void SKGLView_OnPaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            _size = e.BackendRenderTarget.Size;
        }

        private async void SoftButton_OnClicked(object sender, EventArgs e)
        {
            if (RootLayout.Children[1] is GradientGLView current)
            {
                current.Touch -= SKCanvasView_OnTouch;
                current.PaintSurface -= SKGLView_OnPaintSurface;
                RootLayout.Children.RemoveAt(1);

                await Task.Delay(200); // Visual feedback

                AddSoftwareView();
            }
        }

        private async void GpuButton_OnClicked(object sender, EventArgs e)
        {
            if (RootLayout.Children[1] is GradientView current)
            {
                current.Touch -= SKCanvasView_OnTouch;
                current.PaintSurface -= SKCanvasView_OnPaintSurface;
                RootLayout.Children.RemoveAt(1);

                await Task.Delay(200); // Visual feedback

                AddGpuView();
            }
        }

        private void AddSoftwareView()
        {
            var view = new GradientView();
            view.SetBinding(GradientView.GradientSourceProperty, nameof(VM.GradientSource));
            view.SetBinding(GradientView.GradientSizeProperty, nameof(VM.GradientSize));
            view.SetBinding(GradientView.GradientRepeatProperty, nameof(VM.GradientRepeat));
            view.SetBinding(GradientView.EnableTouchEventsProperty, nameof(VM.IsDragEnabled));
            view.SetBinding(GradientView.MaskProperty, MaskPath);
            view.SetBinding(LinearGradient.UseLegacyShaderProperty, UseLegacyShaderPath);

            view.Touch += SKCanvasView_OnTouch;
            view.PaintSurface += SKCanvasView_OnPaintSurface;

            RootLayout.Children.Insert(1, view);
        }

        private void AddGpuView()
        {
            var gpuView = new GradientGLView();
            gpuView.SetBinding(GradientGLView.GradientSourceProperty, nameof(VM.GradientSource));
            gpuView.SetBinding(GradientGLView.GradientSizeProperty, nameof(VM.GradientSize));
            gpuView.SetBinding(GradientGLView.GradientRepeatProperty, nameof(VM.GradientRepeat));
            gpuView.SetBinding(GradientGLView.EnableTouchEventsProperty, nameof(VM.IsDragEnabled));
            gpuView.SetBinding(GradientGLView.MaskProperty, MaskPath);
            gpuView.SetBinding(LinearGradient.UseLegacyShaderProperty, UseLegacyShaderPath);

            gpuView.Touch += SKCanvasView_OnTouch;
            gpuView.PaintSurface += SKGLView_OnPaintSurface;

            RootLayout.Children.Insert(1, gpuView);
        }
    }
}