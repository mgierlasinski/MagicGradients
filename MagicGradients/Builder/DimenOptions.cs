namespace MagicGradients
{
    public class DimenOptions
    {
        public bool IsProportional { get; private set; }

        public DimenOptions Absolute()
        {
            IsProportional = false;
            return this;
        }

        public DimenOptions Proportional()
        {
            IsProportional = true;
            return this;
        }
    }
}
