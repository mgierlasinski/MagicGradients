using MagicGradients.Masks;
using Playground.ViewModels;
using System.Collections.Generic;

namespace Playground.Features.Editor.Handlers
{
    public class MaskHandler : ObservableObject
    {
        public EllipseMask EllipseMask { get; }
        public TextMask TextMask { get; }
        public PathMask PathMask { get; }

        public List<IMask> Masks { get; }

        private IMask _selectedMask;
        public IMask SelectedMask
        {
            get => _selectedMask;
            set => SetProperty(ref _selectedMask, value);
        }

        public List<PathFill> FillModes { get; }

        private PathFill _pathFill;
        public PathFill PathFill
        {
            get => _pathFill;
            set => SetProperty(ref _pathFill, value, () =>
            {
                TextMask.Fill = _pathFill;
                PathMask.Fill = _pathFill;
            });
        }

        public MaskHandler()
        {
            EllipseMask = new EllipseMask();
            TextMask = new TextMask();
            PathMask = new PathMask();

            Masks = new List<IMask>
            {
                null,
                EllipseMask,
                TextMask,
                PathMask
            };

            FillModes = new List<PathFill>
            {
                PathFill.Center,
                PathFill.Fill
            };
        }
    }
}
