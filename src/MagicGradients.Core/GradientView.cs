using System;
using MagicGradients.Masks;

namespace MagicGradients
{
    public partial class GradientView : IGradientControl
    {
        public double ViewWidth { get; private set; }

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetValue(ref _gradientSource, value);
        }

        private Dimensions _gradientSize;
        public Dimensions GradientSize
        {
            get => _gradientSize;
            set => SetValue(ref _gradientSize, value);
        }

        private BackgroundRepeat _gradientRepeat;
        public BackgroundRepeat GradientRepeat
        {
            get => _gradientRepeat;
            set => SetValue(ref _gradientRepeat, value);
        }

        private IGradientMask _mask;
        public IGradientMask Mask
        {
            get => _mask;
            set => SetValue(ref _mask, value);
        }

        private void SetValue<T>(ref T field, T value)
        {
            field = value;
            InvalidateNative();
        }

        partial void InvalidateNative();
    }
}
