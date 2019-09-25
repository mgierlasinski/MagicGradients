namespace MagicGradients.Parser
{
    public static class CssHelpers
    {
        public static double FromDegrees(double degrees) => (180 + degrees) % 360;
    }
}
