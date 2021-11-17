using MagicGradients;
using MagicGradients.Masks;
using Playground.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.Editor.Handlers
{
    public class MaskHandler : ObservableObject
    {
        private readonly ShapePicker _shapePicker;

        public RectangleMask RectangleMask { get; }
        public EllipseMask EllipseMask { get; }
        public TextMask TextMask { get; }
        public PathMask PathMask { get; }
        public MaskCollection Collection { get; }

        public ICommand ShowPickerCommand { get; }

        public MaskHandler()
        {
            _shapePicker = new ShapePicker();

            EllipseMask = new EllipseMask { IsActive = false };

            RectangleMask = new RectangleMask
            {
                IsActive = false,
                Corners = new Corners(Dimensions.Abs(50, 50))
            };

            TextMask = new TextMask
            {
                FontSize = 100,
                Text = "Magic Gradients",
                IsActive = false
            };

            PathMask = new PathMask
            {
                Data = _shapePicker.GetData("Xamagon"),
                IsActive = false
            };

            Collection = new MaskCollection
            {
                Masks = new GradientElements<GradientMask>
                {
                    RectangleMask, EllipseMask, TextMask, PathMask
                }
            };

            ShowPickerCommand = new Command(() => ShowPicker());
        }

        private async Task ShowPicker()
        {
            var selection = await _shapePicker.ShowActionSheet();
            if (selection != null)
            {
                PathMask.Data = selection.Data;
            }
        }
    }
}
