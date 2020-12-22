using MagicGradients;
using MagicGradients.Masks;
using Playground.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.Editor.Handlers
{
    public class MaskHandler : ObservableObject
    {
        private readonly ShapePicker _shapePicker;

        public EllipseMask EllipseMask { get; } = new EllipseMask();
        public TextMask TextMask { get; } = new TextMask();
        public PathMask PathMask { get; } = new PathMask();
        public MaskCollection Collection { get; }

        public List<GradientMask> Masks { get; }

        private GradientMask _selectedMask;
        public GradientMask SelectedMask
        {
            get => _selectedMask;
            set => SetProperty(ref _selectedMask, value, () =>
            {
                if (_selectedMask != null)
                {
                    _clipMode = _selectedMask.ClipMode;
                    RaisePropertyChanged(nameof(ClipMode));
                }

                if (_selectedMask is PathMask pathMask)
                {
                    _pathFill = pathMask.Fill;
                    RaisePropertyChanged(nameof(PathFill));
                }

                RaisePropertyChanged(nameof(MaskState));
            });
        }

        public string MaskState => SelectedMask != null ? SelectedMask.GetType().Name : "None";

        public PathFill[] FillModes { get; }

        private PathFill _pathFill;
        public PathFill PathFill
        {
            get => _pathFill;
            set => SetProperty(ref _pathFill, value, () =>
            {
                if (SelectedMask is PathMask pathMask)
                    pathMask.Fill = _pathFill;
            });
        }

        public ClipMode[] ClipModes { get; }

        private ClipMode _clipMode;
        public ClipMode ClipMode
        {
            get => _clipMode;
            set => SetProperty(ref _clipMode, value, () =>
            {
                if(SelectedMask != null)
                    SelectedMask.ClipMode = _clipMode;
            });
        }

        public FontAttributes[] FontAttributes { get; }
        public ICommand ShowSnippetsCommand { get; }

        public MaskHandler()
        {
            _shapePicker = new ShapePicker();

            Collection = new MaskCollection
            {
                Masks = new GradientElements<GradientMask>
                {
                    EllipseMask, PathMask, TextMask
                }
            };

            Masks = new List<GradientMask>
            {
                null, EllipseMask, TextMask, PathMask, Collection
            };

            FillModes = Enum.GetValues(typeof(PathFill)).Cast<PathFill>().ToArray();
            ClipModes = Enum.GetValues(typeof(ClipMode)).Cast<ClipMode>().ToArray();
            FontAttributes = Enum.GetValues(typeof(FontAttributes)).Cast<FontAttributes>().ToArray();

            ShowSnippetsCommand = new Command(() => ShowSnippetsActionSheet());
        }

        private async Task ShowSnippetsActionSheet()
        {
            var selection = await _shapePicker.ShowActionSheet();
            if (selection != null)
            {
                PathMask.Data = selection.Data;
            }
        }
    }
}
