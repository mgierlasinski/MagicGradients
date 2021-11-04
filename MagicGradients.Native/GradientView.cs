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

        private IGradientMask _mask;
        public IGradientMask Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                InvalidateNative();
            }
        }

        partial void InvalidateNative();
    }
}
