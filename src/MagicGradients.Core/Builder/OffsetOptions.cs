namespace MagicGradients.Builder
{
    public class OffsetOptions
    {
        public bool IsProportional { get; private set; }

        public OffsetOptions Absolute()
        {
            IsProportional = false;
            return this;
        }

        public OffsetOptions Proportional()
        {
            IsProportional = true;
            return this;
        }
    }
}
