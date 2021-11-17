namespace MagicGradients.Builder
{
    public static class GradientBuilderExtensions
    {
        public static IGradientSource BuildSource(this GradientBuilder builder)
        {
            return new GenericGradientSource(builder.Build());
        }

        public static void BuildFor(this GradientBuilder builder, IGradientControl control)
        {
            control.GradientSource = builder.BuildSource();
        }
    }
}
