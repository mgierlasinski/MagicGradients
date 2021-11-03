using MagicGradients.Masks;

namespace MagicGradients
{
    public partial class GradientView
    {
        static GradientView()
        {
            GlobalSetup.Current.UseNativeGradients();
        }

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set
            {
                _gradientSource = value;
                InvalidateNative();
            }
        }

        public Dimensions GradientSize { get; set; }
        public BackgroundRepeat GradientRepeat { get; set; }
        public IGradientMask Mask { get; set; }

        partial void InvalidateNative();
    }
}
