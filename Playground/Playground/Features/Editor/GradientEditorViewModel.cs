using MagicGradients;
using Playground.Features.Editor.Handlers;
using Playground.Features.Gallery.Services;
using Playground.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.Editor
{
    [QueryProperty("Id", "id")]
    public class GradientEditorViewModel : ObservableObject
    {
        private readonly IGalleryService _galleryService;

        public LinearHandler Linear { get; }
        public RadialHandler Radial { get; }

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
        
        private Dimensions _gradientSize = Dimensions.Prop(1, 1);
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

        private bool _isRadial;
        public bool IsRadial
        {
            get => _isRadial;
            set => SetProperty(ref _isRadial, value);
        }

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

        public GradientEditorViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;

            GradientSource = new GradientCollection();
            Linear = new LinearHandler(this);
            Radial = new RadialHandler(this);

            AddCommand = new Command(() => AddAction());
            EditCommand = new Command(EditAction);
            CloseEditCommand = new Command(() => { IsEditMode = false; });

            PreviewCssCommand = new Command(async () =>
            {
                IsMenuVisible = false;
                await Shell.Current.GoToAsync($"CssPreviewer?id={Id}");
            });

            BattleTestCommand = new Command(async () =>
            {
                IsMenuVisible = false;
                await Shell.Current.GoToAsync($"BattleTest?id={Id}");
            });

            ToggleMenuCommand = new Command(() => IsMenuVisible = !IsMenuVisible);
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
    }
}
