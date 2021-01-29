using MagicGradients;
using MagicGradients.Masks;
using Playground.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.Editor.Handlers
{
    public class MaskHandler : ObservableObject
    {
        private readonly ShapePicker _shapePicker;

        public EllipseMask EllipseMask { get; }
        public TextMask TextMask { get; }
        public PathMask PathMask { get; }
        public MaskCollection Collection { get; }

        public FillMode[] FillModes { get; }
        public ClipMode[] ClipModes { get; }
        public FontAttributes[] FontAttributes { get; }

        public ICommand ShowPickerCommand { get; }
        public ICommand ShowEntryCommand { get; }

        public MaskHandler()
        {
            _shapePicker = new ShapePicker();

            FillModes = Enum.GetValues(typeof(FillMode)).Cast<FillMode>().ToArray();
            ClipModes = Enum.GetValues(typeof(ClipMode)).Cast<ClipMode>().ToArray();
            FontAttributes = Enum.GetValues(typeof(FontAttributes)).Cast<FontAttributes>().ToArray();

            EllipseMask = new EllipseMask { IsActive = false };

            TextMask = new TextMask
            {
                FontSize = 100,
                Text = "Magic Gradients",
                IsActive = false
            };

            PathMask = new PathMask
            {
                Data = _shapePicker.Shapes?.FirstOrDefault(x => x.Name == "Xamagon")?.Data,
                IsActive = false
            };

            Collection = new MaskCollection
            {
                Masks = new GradientElements<GradientMask>
                {
                    EllipseMask, TextMask, PathMask
                }
            };

            ShowPickerCommand = new Command(() => ShowPicker());
            ShowEntryCommand = new Command(() => ShowEntry());
        }

        private async Task ShowPicker()
        {
            var selection = await _shapePicker.ShowActionSheet();
            if (selection != null)
            {
                PathMask.Data = selection.Data;
            }
        }

        private async Task ShowEntry()
        {
            var selection = await _shapePicker.ShowEntry();
            if (selection != null)
            {
                PathMask.Data = selection.Data;
            }
        }
    }
}
