using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Editor.Handlers
{
    public class RadialHandler : ObservableObject
    {
        private readonly GradientEditorViewModel _parent;

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

        private double _radiusX;
        public double RadiusX
        {
            get => _radiusX;
            set => SetProperty(ref _radiusX, value, UpdateRadiusX);
        }

        private double _radiusY;
        public double RadiusY
        {
            get => _radiusY;
            set => SetProperty(ref _radiusY, value, UpdateRadiusY);
        }

        private int _shape;
        public int Shape
        {
            get => _shape;
            set => SetProperty(ref _shape, value, UpdateShape);
        }

        private int _sizeOne;
        public int SizeOne
        {
            get => _sizeOne;
            set => SetProperty(ref _sizeOne, value, UpdateSize);
        }

        private int _sizeTwo;
        public int SizeTwo
        {
            get => _sizeTwo;
            set => SetProperty(ref _sizeTwo, value, UpdateSize);
        }

        private bool _isCustomSize;
        public bool IsCustomSize
        {
            get => _isCustomSize;
            set => SetProperty(ref _isCustomSize, value, UpdateIsCustomSize);
        }

        public RadialHandler(GradientEditorViewModel parent)
        {
            _parent = parent;
        }

        public RadialGradient Create()
        {
            var radial = new RadialGradient
            {
                Flags = RadialGradientFlags.PositionProportional
            };

            radial.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            radial.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            radial.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            radial.Measure(0, 0);

            return radial;
        }

        public void LoadGradient(RadialGradient radial)
        {
            _centerX = radial.Center.X;
            _centerY = radial.Center.Y;
            _radiusX = radial.RadiusX;
            _radiusY = radial.RadiusY;
            _shape = (int)radial.Shape;
            _sizeOne = radial.Size.IsClosest() ? 0 : 1;
            _sizeTwo = radial.Size.IsCorner() ? 0 : 1;
            _isCustomSize = RadiusX > 0 || RadiusY > 0;

            RaisePropertyChanged(nameof(CenterX));
            RaisePropertyChanged(nameof(CenterY));
            RaisePropertyChanged(nameof(RadiusX));
            RaisePropertyChanged(nameof(RadiusY));
            RaisePropertyChanged(nameof(Shape));
            RaisePropertyChanged(nameof(SizeOne));
            RaisePropertyChanged(nameof(SizeTwo));
            RaisePropertyChanged(nameof(IsCustomSize));
        }

        private void UpdateCenter()
        {
            if (_parent.Gradient is RadialGradient radial)
                radial.Center = new Point(CenterX, CenterY);
        }

        private void UpdateRadiusX()
        {
            if (!IsCustomSize)
                return;

            if (_parent.Gradient is RadialGradient radial)
                radial.RadiusX = RadiusX;
        }

        private void UpdateRadiusY()
        {
            if (!IsCustomSize)
                return;

            if (_parent.Gradient is RadialGradient radial)
                radial.RadiusY = RadiusY;
        }

        private void UpdateShape()
        {
            if (IsCustomSize)
                return;

            if (_parent.Gradient is RadialGradient radial)
            {
                radial.RadiusX = -1;
                radial.RadiusY = -1;
                radial.Shape = (RadialGradientShape)Shape;
            }
        }

        private void UpdateSize()
        {
            if (IsCustomSize)
                return;

            if (_parent.Gradient is RadialGradient radial)
            {
                radial.RadiusX = -1;
                radial.RadiusY = -1;
                radial.Size = SizeOne == 0 ? 
                    SizeTwo == 0 ? RadialGradientSize.ClosestCorner : RadialGradientSize.ClosestSide :
                    SizeTwo == 0 ? RadialGradientSize.FarthestCorner : RadialGradientSize.FarthestSide;
            }
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
