using MagicGradients;
using Playground.Models;
using Playground.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.ViewModels
{
    [QueryProperty("Id", "id")]
    public class GalleryPreviewViewModel : BaseViewModel
    {
        private readonly IGalleryService _galleryService;

        public ICommand EditCommand { get; set; }

        public ICommand PreviewCssCommand { get; set; }

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value);
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
                GradientSource = _galleryService.GetGradientById(int.Parse(_id)).Source;
                EditorItems = GradientSource.GetGradients().Select(x => new GradientEditorItem
                {
                    StopsSource = new LinearGradient { Angle = 270, Stops = x.Stops },
                    Type = x.GetType().Name
                }).ToList();
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

            EditCommand = new Command(() => { IsEditMode = !IsEditMode; });

            PreviewCssCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"PasteCss?id={Id}");
            });
        }
    }
}
