namespace MagicGradients.Builder
{
    public class LinearGradientBuilder : StopsBuilder<LinearGradientBuilder>, IChildBuilder
    {
        protected override LinearGradientBuilder Instance => this;

        internal double Angle { get; set; }
        internal bool IsRepeating { get; set; }

        public LinearGradientBuilder()
        {
            Angle = 0;
            IsRepeating = false;
        }

        public LinearGradientBuilder Rotate(double angle)
        {
            Angle = angle;
            return this;
        }

        public LinearGradientBuilder Repeat()
        {
            IsRepeating = true;
            return this;
        }

        public IGradient Construct()
        {
            return Factory.Construct(this);
        }
    }
}
