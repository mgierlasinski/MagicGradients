using System.Collections.Generic;
using MagicGradients;
using Playground.Features.Editor.Handlers;
using Playground.Features.Gallery.Services;
using Playground.ViewModels;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicGradients.Masks;
using Playground.Features.Share;
using Xamarin.Forms;

namespace Playground.Features.Editor
{
    [QueryProperty("Id", "id")]
    public class GradientEditorViewModel : ObservableObject
    {
        private readonly IGalleryService _galleryService;
        private readonly IShareService _shareService;
        private readonly IGradientExporter _exporter;

        public LinearHandler Linear { get; }
        public RadialHandler Radial { get; }

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

        private GradientCollection _gradientSource;
        public GradientCollection GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value);
        }

        private Gradient _gradient;
        public Gradient Gradient
        {
            get => _gradient;
            set => SetProperty(ref _gradient, value, OnGradientChanged);
        }
        
        private Dimensions _gradientSize;
        public Dimensions GradientSize
        {
            get => _gradientSize;
            set => SetProperty(ref _gradientSize, value, UpdateSizeOnUI);
        }

        private BackgroundRepeat _gradientRepeat;
        public BackgroundRepeat GradientRepeat
        {
            get => _gradientRepeat;
            set => SetProperty(ref _gradientRepeat, value);
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        private double _sizeScale = 1;
        public double SizeScale
        {
            get => _sizeScale;
            set => SetProperty(ref _sizeScale, value, UpdateSize);
        }

        private double _sizeWidth = 100;
        public double SizeWidth
        {
            get => _sizeWidth;
            set => SetProperty(ref _sizeWidth, value, UpdateSize);
        }

        private double _sizeHeight = 100;
        public double SizeHeight
        {
            get => _sizeHeight;
            set => SetProperty(ref _sizeHeight, value, UpdateSize);
        }

        private bool _isPixelSize;
        public bool IsPixelSize
        {
            get => _isPixelSize;
            set => SetProperty(ref _isPixelSize, value, UpdateSize);
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value, 
                () => RaisePropertyChanged(nameof(IsDragEnabled)));
        }

        private bool _isRadial;
        public bool IsRadial
        {
            get => _isRadial;
            set => SetProperty(ref _isRadial, value, 
                () => RaisePropertyChanged(nameof(IsDragEnabled)));
        }

        public bool IsDragEnabled => IsEditMode && IsRadial;

        private bool _isGallery;
        public bool IsGallery
        {
            get => _isGallery;
            set => SetProperty(ref _isGallery, value);
        }

        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set => SetProperty(ref _isMenuVisible, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CloseEditCommand { get; }
        public ICommand PreviewCssCommand { get; }
        public ICommand BattleTestCommand { get; }
        public ICommand ToggleMenuCommand { get; }
        public ICommand ShareCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand RotateCommand { get; }

        public EllipseMask EllipseMask { get; }
        public TextMask TextMask { get; }
        public List<IMask> Masks { get; }
        public List<TextMaskFill> FillModes { get; }

        private IMask _selectedMask;
        public IMask SelectedMask
        {
            get => _selectedMask;
            set => SetProperty(ref _selectedMask, value);
        }

        public GradientEditorViewModel(
            IGalleryService galleryService, 
            IShareService shareService,
            IGradientExporter exporter)
        {
            _galleryService = galleryService;
            _shareService = shareService;
            _exporter = exporter;

            GradientSource = new GradientCollection();
            Linear = new LinearHandler(this);
            Radial = new RadialHandler(this);

            AddCommand = new Command(() => AddAction());
            EditCommand = new Command(EditAction);
            CloseEditCommand = new Command(() => { IsEditMode = false; });

            PreviewCssCommand = new Command(async () =>
            {
                IsMenuVisible = false;
                await Shell.Current.GoToAsync($"CssPreviewer?data={GetRawData()}");
            });

            BattleTestCommand = new Command(async () =>
            {
                IsMenuVisible = false;
                await Shell.Current.GoToAsync($"BattleTest?data={GetRawData()}");
            });

            ToggleMenuCommand = new Command(() => IsMenuVisible = true);
            ShareCommand = new Command(() =>
            {
                IsMenuVisible = false;
                _shareService.ShareText("Share Gradient", GetShareText());
            });
            CopyCommand = new Command(() =>
            {
                IsMenuVisible = false;
                _shareService.CopyToClipboard(GetShareText());
            });
            RotateCommand = new Command<string>((x) =>
            {
                if (Gradient is LinearGradient linear)
                    linear.Angle = double.Parse(x);
            });

            EllipseMask = new EllipseMask();
            TextMask = new TextMask();
            Masks = new List<IMask>
            {
                null,
                EllipseMask,
                TextMask
            };
            FillModes = new List<TextMaskFill>
            {
                TextMaskFill.Center,
                TextMaskFill.CenterAndScale,
                TextMaskFill.Fill
            };
        }

        private void LoadGradient()
        {
            if (int.TryParse(_id, out var id))
            {
                LoadFromGallery(id);   
                return;
            }

            if (_id == "linear")
                GradientSource.Gradients.Add(Linear.Create());

            if (_id == "radial")
                GradientSource.Gradients.Add(Radial.Create());

            GradientSize = Dimensions.Prop(1, 1);
            EditCommand.Execute(null);
        }

        private void LoadFromGallery(int id)
        {
            var gradient = _galleryService.GetGradientById(id);
            GradientSource = (GradientCollection)gradient.Source;
            GradientSize = gradient.Size;
            IsGallery = true;
        }

        private void OnGradientChanged()
        {
            if (_gradient is RadialGradient radial)
            {
                Radial.LoadGradient(radial);
                IsRadial = true;
            }
            else
            {
                IsRadial = false;
            }
        }

        private async Task AddAction()
        {
            var values = new [] { "Linear gradient", "Radial gradient" };
            var result = await Shell.Current.DisplayActionSheet("Add gradient", "Cancel", null, values);

            if(result == values[0])
                GradientSource.Gradients.Add(Linear.Create());

            else if (result == values[1])
                GradientSource.Gradients.Add(Radial.Create());

            // Select added
            Gradient = GradientSource?.Gradients?.LastOrDefault();
        }

        private void EditAction()
        {
            if (Gradient == null)
                Gradient = GradientSource.Gradients.FirstOrDefault();

            IsEditMode = true;
        }

        private void UpdateSize()
        {
            GradientSize = IsPixelSize
                ? Dimensions.Abs(SizeWidth, SizeHeight)
                : Dimensions.Prop(SizeScale, SizeScale);
        }

        private void UpdateSizeOnUI()
        {
            if (GradientSize.IsZero)
                return;

            if (GradientSize.Width.Type == OffsetType.Absolute)
            {
                _isPixelSize = true;    
                _sizeWidth = GradientSize.Width.Value;
                _sizeHeight = GradientSize.Height.Value;
            }
            else
            {
                _isPixelSize = false;
                _sizeScale = GradientSize.Width.Value;
            }

            // Notify UI only, don't raise OnChanged action
            RaisePropertyChanged(nameof(IsPixelSize));
            RaisePropertyChanged(nameof(SizeScale));
            RaisePropertyChanged(nameof(SizeWidth));
            RaisePropertyChanged(nameof(SizeHeight));
        }

        private string GetRawData()
        {
            var data = new ExportData
            {
                GradientSource = GradientSource,
                GradientSize = GradientSize,
                GradientRepeat = GradientRepeat
            };

            return _exporter.ExportRaw(data);
        }

        private string GetShareText()
        {
            var data = new ExportData
            {
                GradientSource = GradientSource,
                GradientSize = GradientSize,
                GradientRepeat = GradientRepeat
            };

            return _exporter.ExportCss(data);
        }
    }
}
