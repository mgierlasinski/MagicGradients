using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using System.Windows.Input;
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

        private Dimensions _radius;
        public Dimensions Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value, UpdateRadius);
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

        public ICommand MoveToCommand { get; }

        public RadialHandler(GradientEditorViewModel parent)
        {
            _parent = parent;

            MoveToCommand = new Command<string>((x) =>
            {
                var values = x.Split(';');
                CenterX = double.Parse(values[0]);
                CenterY = double.Parse(values[1]);
            });
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
            _shape = (int)radial.Shape;
            _sizeOne = radial.Size.IsClosest() ? 0 : 1;
            _sizeTwo = radial.Size.IsCorner() ? 0 : 1;
            _isCustomSize = radial.RadiusX > 0 || radial.RadiusY > 0;

            LoadRadius(radial);

            // Notify UI only, don't raise OnChanged action
            RaisePropertyChanged(nameof(CenterX));
            RaisePropertyChanged(nameof(CenterY));
            RaisePropertyChanged(nameof(Radius));
            RaisePropertyChanged(nameof(Shape));
            RaisePropertyChanged(nameof(SizeOne));
            RaisePropertyChanged(nameof(SizeTwo));
            RaisePropertyChanged(nameof(IsCustomSize));
        }

        private void LoadRadius(RadialGradient radial)
        {
            var flags = radial.Flags;
            var widthProp = FlagsHelper.IsSet(flags, RadialGradientFlags.WidthProportional);
            var heightProp = FlagsHelper.IsSet(flags, RadialGradientFlags.HeightProportional);

            var radiusX = radial.RadiusX > 0
                ? new Offset(radial.RadiusX, widthProp ? OffsetType.Proportional : OffsetType.Absolute)
                : Offset.Zero;

            var radiusY = radial.RadiusY > 0
                ? new Offset(radial.RadiusY, heightProp ? OffsetType.Proportional : OffsetType.Absolute)
                : Offset.Zero;

            _radius = new Dimensions(radiusX, radiusY);

            if (_radius.IsZero)
                _radius = Dimensions.Prop(0.5, 0.5);
        }

        private void UpdateCenter()
        {
            if (_parent.Gradient is RadialGradient radial)
                radial.Center = new Point(CenterX, CenterY);
        }

        private void UpdateRadius()
        {
            if (!IsCustomSize)
                return;

            if (_parent.Gradient is RadialGradient radial)
            {
                radial.RadiusX = Radius.Width.Value;
                radial.RadiusY = Radius.Height.Value;

                var flags = radial.Flags;

                FlagsHelper.SetValue(ref flags, RadialGradientFlags.WidthProportional, Radius.Width.Type == OffsetType.Proportional);
                FlagsHelper.SetValue(ref flags, RadialGradientFlags.HeightProportional, Radius.Height.Type == OffsetType.Proportional);

                radial.Flags = flags;
            }
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
                UpdateRadius();
            }
            else
            {
                UpdateShape();
                UpdateSize();
            }
        }
    }
}
