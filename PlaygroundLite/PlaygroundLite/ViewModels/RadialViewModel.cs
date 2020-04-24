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
            set => SetProperty(ref _centerX, value, onChanged: UpdateCenter);
        }

        private double _centerY = 0.5d;
        public double CenterY
        {
            get => _centerY;
            set => SetProperty(ref _centerY, value, onChanged: UpdateCenter);
        }

        private double _radiusX = 200;
        public double RadiusX
        {
            get => _radiusX;
            set => SetProperty(ref _radiusX, value, onChanged: UpdateRadiusX);
        }

        private double _radiusY = 200;
        public double RadiusY
        {
            get => _radiusY;
            set => SetProperty(ref _radiusY, value, onChanged: UpdateRadiusY);
        }

        public RadialGradientShape[] Shapes { get; set; } =
        {
            RadialGradientShape.Circle, RadialGradientShape.Ellipse
        };

        public RadialGradientSize[] Sizes { get; set; } =
        {
            RadialGradientSize.ClosestCorner, 
            RadialGradientSize.ClosestSide,
            RadialGradientSize.FarthestCorner, 
            RadialGradientSize.FarthestSide
        };

        private RadialGradientShape _selectedShape = RadialGradientShape.Circle;
        public RadialGradientShape SelectedShape
        {
            get => _selectedShape;
            set => SetProperty(ref _selectedShape, value, onChanged: UpdateShape);
        }

        private RadialGradientSize _selectedSize = RadialGradientSize.ClosestSide;
        public RadialGradientSize SelectedSize
        {
            get => _selectedSize;
            set => SetProperty(ref _selectedSize, value, onChanged: UpdateSize);
        }

        private bool _isCustomSize;
        public bool IsCustomSize
        {
            get => _isCustomSize;
            set => SetProperty(ref _isCustomSize, value, onChanged: UpdateIsCustomSize);
        }

        public RadialViewModel()
        {
            Gradient = new RadialGradient
            {
                Flags = RadialGradientFlags.PositionProportional
            };

            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            Gradient.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });

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
