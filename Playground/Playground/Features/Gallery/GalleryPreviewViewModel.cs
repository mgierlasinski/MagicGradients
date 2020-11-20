using MagicGradients;
using Playground.Features.Gallery.Services;
using Playground.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.Gallery
{
    [QueryProperty("Id", "id")]
    public class GalleryPreviewViewModel : ObservableObject
    {
        private readonly IGalleryService _galleryService;

        public ICommand EditCommand { get; set; }
        public ICommand CloseEditCommand { get; set; }
        public ICommand PreviewCssCommand { get; set; }
        public ICommand BattleTestCommand { get; set; }

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

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value);
        }

        private Dimensions _gradientSize;
        public Dimensions GradientSize
        {
            get => _gradientSize;
            set => SetProperty(ref _gradientSize, value);
        }

        private List<Gradient> _gradients;
        public List<Gradient> Gradients
        {
            get => _gradients;
            set => SetProperty(ref _gradients, value);
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value, 
                () => RaisePropertyChanged(nameof(SelectedGradient)));
        }

        public Gradient SelectedGradient => SelectedIndex >= 0 && SelectedIndex < Gradients.Count 
            ? Gradients[SelectedIndex] : null;
        
        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        public GalleryPreviewViewModel(IGalleryService galleryService)
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
            var gradient = _galleryService.GetGradientById(int.Parse(_id));
            GradientSource = gradient.Source;
            GradientSize = gradient.Size;

            Gradients = GradientSource.GetGradients().ToList();
        }

        private void EditAction()
        {
            if (SelectedIndex < 0)
                SelectedIndex = 0;

            IsEditMode = true;
        }
    }
}
