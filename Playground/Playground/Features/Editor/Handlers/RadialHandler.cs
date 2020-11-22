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

        public RadialHandler(GradientEditorViewModel parent)
        {
            _parent = parent;
        }

        public Gradient Create()
        {
            var radial = new RadialGradient
            {
                Flags = RadialGradientFlags.PositionProportional
            };

            radial.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            radial.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            radial.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            radial.Measure(0, 0);

            UpdateCenter();
            UpdateShape();
            UpdateSize();

            return radial;
        }

        public void LoadGradient(RadialGradient radial)
        {
            CenterX = radial.Center.X;
            CenterY = radial.Center.Y;
            RadiusX = radial.RadiusX;
            RadiusY = radial.RadiusY;
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
                radial.Shape = SelectedShape;
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
                radial.Size = SelectedSize;
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
