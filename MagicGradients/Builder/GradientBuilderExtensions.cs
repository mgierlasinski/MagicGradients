namespace MagicGradients
{
    public static class GradientBuilderExtensions
    {
        public static IGradientSource ToSource(this GradientBuilder builder)
        {
            return new GradientCollection
            {
                Gradients = new GradientElements<Gradient>(builder.Build())
            };
        }
    }
}
