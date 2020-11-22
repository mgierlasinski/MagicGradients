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

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CloseEditCommand { get; }
        public ICommand PreviewCssCommand { get; }
        public ICommand BattleTestCommand { get; }

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
                GradientSource = (GradientCollection)gradient.Source;
                GradientSize = gradient.Size;
                return;
            }

            if (_id == "linear")
                GradientSource.Gradients.Add(Linear.Create());

            if (_id == "radial")
                GradientSource.Gradients.Add(Radial.Create());

            EditCommand.Execute(null);
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
