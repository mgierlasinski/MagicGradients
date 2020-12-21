using MagicGradients.Masks;
using Playground.ViewModels;
using System.Collections.Generic;
using MagicGradients;
using Xamarin.Forms;

namespace Playground.Features.Editor.Handlers
{
    public class MaskHandler : ObservableObject
    {
        public EllipseMask EllipseMask { get; }
        public TextMask TextMask { get; }
        public PathMask PathMask { get; }
        public MaskCollection Collection { get; }

        public List<GradientMask> Masks { get; }

        private GradientMask _selectedMask;
        public GradientMask SelectedMask
        {
            get => _selectedMask;
            set => SetProperty(ref _selectedMask, value, () =>
            {
                _clipMode = _selectedMask.ClipMode;
                RaisePropertyChanged(nameof(ClipMode));

                if (_selectedMask is PathMask pathMask)
                {
                    _pathFill = pathMask.Fill;
                    RaisePropertyChanged(nameof(PathFill));
                }

                RaisePropertyChanged(nameof(IsPathMask));
                RaisePropertyChanged(nameof(IsTextMask));
            });
        }

        public List<PathFill> FillModes { get; }

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

        public List<ClipMode> ClipModes { get; }

        private ClipMode _clipMode;
        public ClipMode ClipMode
        {
            get => _clipMode;
            set => SetProperty(ref _clipMode, value, () =>
            {
                SelectedMask.ClipMode = _clipMode;
            });
        }

        public List<FontAttributes> FontAttributes { get; }

        public bool IsPathMask => SelectedMask is PathMask;
        public bool IsTextMask => SelectedMask is TextMask;
        
        public MaskHandler()
        {
            EllipseMask = new EllipseMask();
            TextMask = new TextMask();
            PathMask = new PathMask();

            Collection = new MaskCollection
            {
                Masks = new GradientElements<GradientMask>
                {
                    EllipseMask,
                    PathMask,
                    TextMask
                }
            };

            Masks = new List<GradientMask>
            {
                null,
                EllipseMask,
                TextMask,
                PathMask,
                Collection
            };

            FillModes = new List<PathFill>
            {
                PathFill.Center,
                PathFill.Fill
            };

            ClipModes = new List<ClipMode>
            {
                ClipMode.Intersect,
                ClipMode.Difference
            };

            FontAttributes = new List<FontAttributes>
            {
                Xamarin.Forms.FontAttributes.None,
                Xamarin.Forms.FontAttributes.Bold,
                Xamarin.Forms.FontAttributes.Italic
            };
        }
    }
}
