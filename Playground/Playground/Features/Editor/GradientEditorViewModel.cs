using MagicGradients;
using Playground.Features.Editor.Handlers;
using Playground.Features.Gallery.Services;
using Playground.Features.Share;
using Playground.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicGradients.Parser;
using Xamarin.Forms;
using System;

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
        public MaskHandler Mask { get; }

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
            set => SetProperty(ref _gradientSize, value);
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

        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set => SetProperty(ref _isMenuVisible, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CloseEditCommand { get; }
        public ICommand PreviewCssCommand { get; }
        public ICommand BattleTestCommand { get; }
        public ICommand ToggleMenuCommand { get; }
        public ICommand ShareCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        
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
            Mask = new MaskHandler();

            AddCommand = new Command(() => AddAction());
            EditCommand = new Command(EditAction);
            DeleteCommand = new Command(() => DeleteAction());
            CloseEditCommand = new Command(() => IsEditMode = false);
            ToggleMenuCommand = new Command(() => IsMenuVisible = true);
            MoveUpCommand = new Command(() => MoveAction(1));
            MoveDownCommand = new Command(() => MoveAction(-1));

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
            var values = new [] { "Linear gradient", "Radial gradient", "CSS gradient" };
            var result = await Shell.Current.DisplayActionSheet("Add gradient", "Cancel", null, values);

            if(result == values[0])
                GradientSource.Gradients.Add(Linear.Create());

            else if (result == values[1])
                GradientSource.Gradients.Add(Radial.Create());

            else if (result == values[2])
                await LoadCssSource();

            // Select added
            Gradient = GradientSource?.Gradients?.LastOrDefault();
        }

        private async Task LoadCssSource()
        {
            try
            {
                var code = await Shell.Current.DisplayPromptAsync("CSS gradient", "Enter CSS code");

                if (string.IsNullOrWhiteSpace(code))
                    return;

                var parser = new CssGradientParser();
                var gradients = parser.ParseCss(code);

                if (!gradients.Any())
                {
                    await Shell.Current.DisplayAlert("CSS gradient", "No gradient data", "OK");
                    return;
                }

                foreach (var gradient in gradients)
                {
                    GradientSource.Gradients.Add(gradient);
                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("CSS gradient", e.Message, "OK");
            }
        }

        private void EditAction()
        {
            Gradient ??= GradientSource.Gradients.FirstOrDefault();
            IsEditMode = true;
        }

        private async Task DeleteAction()
        {
            if (Gradient == null || GradientSource.Gradients.Count == 1)
                return;

            if (!await Shell.Current.DisplayAlert("Confirmation", "Delete selected gradient?", "Delete", "Cancel"))
                return;

            var index = GradientSource.Gradients.IndexOf(Gradient);
            if (index < 0)
                return;

            GradientSource.Gradients.RemoveAt(index);

            if (index >= GradientSource.Gradients.Count)
                index = GradientSource.Gradients.Count - 1;

            Gradient = GradientSource.Gradients[index];
        }

        private void MoveAction(int direction)
        {
            if(Gradient == null || GradientSource.Gradients.Count == 1)
                return;

            var index = GradientSource.Gradients.IndexOf(Gradient);
            var newIndex = index + direction;

            if (newIndex < 0 || newIndex > GradientSource.Gradients.Count - 1)
                return;

            var movedGradient = Gradient;
            GradientSource.Gradients.RemoveAt(index);
            GradientSource.Gradients.Insert(newIndex, movedGradient);
            Gradient = movedGradient;
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
