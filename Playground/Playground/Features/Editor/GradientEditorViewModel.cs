using MagicGradients;
using Playground.Extensions;
using Playground.Features.Gallery.Services;
using Playground.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Editor
{
    [QueryProperty("Id", "id")]
    public class GradientEditorViewModel : ObservableObject
    {
        private readonly IGalleryService _galleryService;

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value, 
                () => Gradients = GradientSource?.GetGradients()?.ToList());
        }

        private List<Gradient> _gradients;
        public List<Gradient> Gradients
        {
            get => _gradients;
            set => SetProperty(ref _gradients, value);
        }

        private Gradient _gradient;
        public Gradient Gradient
        {
            get => _gradient;
            set => SetProperty(ref _gradient, value, 
                () => RaisePropertyChanged(nameof(IsRadial)));
        }

        public bool IsRadial => Gradient is RadialGradient;

        private int _isRepeatingIndex;
        public int IsRepeatingIndex
        {
            get => _isRepeatingIndex;
            set => SetProperty(ref _isRepeatingIndex, value,
                () => Gradient.IsRepeating = _isRepeatingIndex == 1);
        }

        private Dimensions _gradientSize = Dimensions.Prop(1, 1);
        public Dimensions GradientSize
        {
            get => _gradientSize;
            set => SetProperty(ref _gradientSize, value);
        }

        public BackgroundRepeat GradientRepeat => (BackgroundRepeat)RepeatIndex;

        private int _repeatIndex;
        public int RepeatIndex
        {
            get => _repeatIndex;
            set => SetProperty(ref _repeatIndex, value, 
                () => RaisePropertyChanged(nameof(GradientRepeat)));
        }

        //private double _angle;
        //public double Angle
        //{
        //    get => _angle;
        //    set => SetProperty(ref _angle, value, UpdateAngle);
        //}

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

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadGradient();
            }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        public ICommand EditCommand { get; set; }
        public ICommand CloseEditCommand { get; set; }
        public ICommand PreviewCssCommand { get; set; }
        public ICommand BattleTestCommand { get; set; }

        public GradientEditorViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;

            EditCommand = new Command(EditAction);
            CloseEditCommand = new Command(() => { IsEditMode = false; });
            PreviewCssCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"CssPreviewer?id={Id}");
            });
            BattleTestCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"BattleTest?id={Id}");
            });
        }

        private void LoadGradient()
        {
            if (int.TryParse(_id, out var id))
            {
                var gradient = _galleryService.GetGradientById(id);
                GradientSource = gradient.Source;
                GradientSize = gradient.Size;
            }

            if (_id == "linear")
            {
                GradientSource = GetLinearGradient();
                EditCommand.Execute(null);
            }

            if (_id == "radial")
            {
                GradientSource = GetRadialGradient();
                EditCommand.Execute(null);
            }
        }

        private IGradientSource GetLinearGradient()
        {
            var linear = new LinearGradient();
            linear.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            linear.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            linear.Stops.Add(new GradientStop { Color = ColorUtils.GetRandom() });
            linear.Measure(0, 0);

            return linear;
        }

        private IGradientSource GetRadialGradient()
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

        private void EditAction()
        {
            if (Gradient == null)
                Gradient = Gradients.FirstOrDefault();

            IsEditMode = true;
        }

        //protected void UpdateAngle()
        //{
        //    if(Gradient is LinearGradient linear)
        //        linear.Angle = _angle;
        //}

        protected void UpdateCenter()
        {
            if (Gradient is RadialGradient radial)
                radial.Center = new Point(CenterX, CenterY);
        }

        protected void UpdateRadiusX()
        {
            if (!IsCustomSize)
                return;

            if (Gradient is RadialGradient radial)
                radial.RadiusX = RadiusX;
        }

        protected void UpdateRadiusY()
        {
            if (!IsCustomSize)
                return;

            if (Gradient is RadialGradient radial)
                radial.RadiusY = RadiusY;
        }

        protected void UpdateShape()
        {
            if (IsCustomSize)
                return;

            if (Gradient is RadialGradient radial)
            {
                radial.RadiusX = -1;
                radial.RadiusY = -1;
                radial.Shape = SelectedShape;
            }
        }

        protected void UpdateSize()
        {
            if (IsCustomSize)
                return;

            if (Gradient is RadialGradient radial)
            {
                radial.RadiusX = -1;
                radial.RadiusY = -1;
                radial.Size = SelectedSize;
            }
        }

        protected void UpdateIsCustomSize()
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
