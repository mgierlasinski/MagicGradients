using System.Linq;
using MagicGradients;
using PlaygroundLite.Services;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class RadialViewModel : GradientViewModel<RadialGradient>
    {
        private double _centerX = 0.5d;
        public double CenterX
        {
            get => _centerX;
            set => SetProperty(ref _centerX, value, UpdateCenter);
        }

        private double _centerY = 0.5d;
        public double CenterY
        {
            get => _centerY;
            set => SetProperty(ref _centerY, value, UpdateCenter);
        }

        private double _radiusX = 200;
        public double RadiusX
        {
            get => _radiusX;
            set => SetProperty(ref _radiusX, value, UpdateRadiusX);
        }

        private double _radiusY = 200;
        public double RadiusY
        {
            get => _radiusY;
            set => SetProperty(ref _radiusY, value, UpdateRadiusY);
        }

        public RadialGradientShape SelectedShape => (RadialGradientShape)ShapeIndex;

        private int _shapeIndex;
        public int ShapeIndex
        {
            get => _shapeIndex;
            set => SetProperty(ref _shapeIndex, value, UpdateShape);
        }

        public RadialGradientSize SelectedSize
        {
            get
            {
                if (SizeOneIndex == 0)
                    return SizeTwoIndex == 0 ? RadialGradientSize.ClosestCorner : RadialGradientSize.ClosestSide;

                return SizeTwoIndex == 0 ? RadialGradientSize.FarthestCorner : RadialGradientSize.FarthestSide;
            }
        }

        private int _sizeOneIndex;
        public int SizeOneIndex
        {
            get => _sizeOneIndex;
            set => SetProperty(ref _sizeOneIndex, value, UpdateSize);
        }

        private int _sizeTwoIndex;
        public int SizeTwoIndex
        {
            get => _sizeTwoIndex;
            set => SetProperty(ref _sizeTwoIndex, value, UpdateSize);
        }

        private bool _isCustomSize;
        public bool IsCustomSize
        {
            get => _isCustomSize;
            set => SetProperty(ref _isCustomSize, value, UpdateIsCustomSize);
        }

        public RadialViewModel()
        {
            InitializeGradient();

            ResetCommand = new Command(InitializeGradient);
        }

        private void InitializeGradient()
        {
            Gradient = new RadialGradient
            {
                Flags = RadialGradientFlags.PositionProportional
            };

            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });

            SelectedStop = Gradient.Stops.First();

            UpdateStopsCount();
            UpdateCenter();
            UpdateShape();
            UpdateSize();
        }

        private void UpdateCenter() => Gradient.Center = new Point(CenterX, CenterY);

        private void UpdateRadiusX()
        {
            if (!IsCustomSize)
                return;

            Gradient.RadiusX = RadiusX;
        }

        private void UpdateRadiusY()
        {
            if (!IsCustomSize)
                return;

            Gradient.RadiusY = RadiusY;
        }

        private void UpdateShape()
        {
            if (IsCustomSize)
                return;

            Gradient.RadiusX = -1;
            Gradient.RadiusY = -1;
            Gradient.Shape = SelectedShape;
        }

        private void UpdateSize()
        {
            if (IsCustomSize)
                return;

            Gradient.RadiusX = -1;
            Gradient.RadiusY = -1;
            Gradient.Size = SelectedSize;
        }

        private void UpdateIsCustomSize()
        {
            if (IsCustomSize)
            {
                UpdateRadiusX();
                UpdateRadiusY();
            }
            else
            {
                UpdateShape();
                UpdateSize();
            }
        }
    }
}
