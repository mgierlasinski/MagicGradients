using MagicGradients.Builder;

namespace MagicGradients
{
    public class GlobalSetup
    {
        public static GlobalSetup Current { get; } = new GlobalSetup();

        public IGradientFactory GradientFactory { get; private set; } = new GradientFactory();

        public GlobalSetup UseFactory(IGradientFactory factory)
        {
            GradientFactory = factory;
            return this;
        }
    }
}
