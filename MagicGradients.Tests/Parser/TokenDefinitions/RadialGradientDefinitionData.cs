using Xamarin.Forms;
using Xunit;
using static MagicGradients.RadialGradientShape;
using static MagicGradients.RadialGradientSize;
using static MagicGradients.RadialGradientFlags;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class RadialGradientDefinitionData : TheoryData<string, RadialGradient>
    {
        public RadialGradientDefinitionData()
        {
            AddDefinitions();
            AddShapes();
            AddSizes();
            AddPositions();
        }

        private void AddDefinitions()
        {
            Add("radial-gradient", new RadialGradient());
            Add(" radial-gradient ", new RadialGradient());
            Add("repeating-radial-gradient", new RadialGradient { IsRepeating = true });
            Add(" repeating-radial-gradient ", new RadialGradient { IsRepeating = true });
        }

        private void AddShapes()
        {
            Add("radial-gradient(circle", new RadialGradient { Shape = Circle });
            Add("radial-gradient(ellipse", new RadialGradient { Shape = Ellipse });
        }

        private void AddSizes()
        {
            Add("radial-gradient(closest-side", new RadialGradient { Size = ClosestSide });
            Add("radial-gradient(closest-corner", new RadialGradient { Size = ClosestCorner });
            Add("radial-gradient(farthest-side", new RadialGradient { Size = FarthestSide });
            Add("radial-gradient(farthest-corner", new RadialGradient { Size = FarthestCorner });
        }

        private void AddPositions()
        {
            Add("radial-gradient(at 150px 150px", RadialAt(150, 150, None));
            Add("radial-gradient(at 1px 1px", RadialAt(1, 1, None));
            Add("radial-gradient(at 1% 1%", RadialAt(0.01, 0.01));
            Add("radial-gradient(at 50% 50%", RadialAt(0.5, 0.5));
            Add("radial-gradient(at top left", RadialAt(0, 0));
            Add("radial-gradient(at top right", RadialAt(1, 0));
            Add("radial-gradient(at bottom left", RadialAt(0, 1));
            Add("radial-gradient(at bottom right", RadialAt(1, 1));
            Add("radial-gradient(at top center", RadialAt(0.5, 0));
            Add("radial-gradient(at right center", RadialAt(1, 0.5));
        }

        private RadialGradient RadialAt(double x, double y, RadialGradientFlags? flags = null)
        {
            return new RadialGradient
            {
                Center = new Point(x, y),
                Flags = flags ?? RadialGradientFlags.PositionProportional
            };
        }
    }
}
