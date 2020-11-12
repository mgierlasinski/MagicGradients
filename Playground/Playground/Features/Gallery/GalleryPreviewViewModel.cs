using MagicGradients;
using Playground.Features.Gallery.Models;
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
        public ICommand SelectCommand { get; set; }

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

        private List<GradientEditorItem> _editorItems;
        public List<GradientEditorItem> EditorItems
        {
            get => _editorItems;
            set => SetProperty(ref _editorItems, value);
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadGradientPreview();
            }
        }

        private GradientEditorItem _selectedItem;
        public GradientEditorItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        public GalleryPreviewViewModel(IGalleryService galleryService)
        {
            _galleryService = galleryService;

            EditCommand = new Command(() => { IsEditMode = true; });
            CloseEditCommand = new Command(() => { IsEditMode = false; });
            PreviewCssCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"CssPreviewer?id={Id}");
            });
            BattleTestCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"BattleTest?id={Id}");
            });
            SelectCommand = new Command<GradientEditorStop>(stop =>
            {
                if (SelectedItem == null)
                    return;

                SelectedItem.SelectedStop = stop;
            });
        }

        private void LoadGradientPreview()
        {
            var gradient = _galleryService.GetGradientById(int.Parse(_id));
            GradientSource = gradient.Source;
            GradientSize = gradient.Size;

            EditorItems = GradientSource.GetGradients().Select(GradientEditorItem.FromGradient).ToList();
            SelectedItem = EditorItems.FirstOrDefault();
        }
    }
}
