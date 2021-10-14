using Android.Content;
using MagicGradients.Forms;
using MagicGradients.Forms.Android;
using Microsoft.Maui.Graphics.Native;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GraphicsView), typeof(GraphicsViewRenderer))]
namespace MagicGradients.Forms.Android
{
    [Preserve]
    public class GraphicsViewRenderer : ViewRenderer<GraphicsView, NativeGraphicsView>
    {
        public GraphicsViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<GraphicsView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                SetNativeControl(null);
            }

            if (e.NewElement != null)
            {
                SetNativeControl(new NativeGraphicsView(Context));
                UpdateDrawable();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(GraphicsView.Drawable))
                UpdateDrawable();
        }

        private void UpdateDrawable()
        {
            Control.Drawable = Element.Drawable;
        }
    }
}
