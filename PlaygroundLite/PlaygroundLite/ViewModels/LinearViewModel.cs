using MagicGradients;

namespace PlaygroundLite.ViewModels
{
    public class LinearViewModel : GradientViewModel<LinearGradient>
    {
        private float _angle;
        public float Angle
        {
            get => _angle;
            set => SetProperty(ref _angle, value, 
                onChanged: () => Gradient.Angle = _angle);
        }

        public LinearViewModel()
        {
            Gradient = new LinearGradient();
            Gradient.Stops.Add(new GradientStop { Color = GetRandomColor() });
            Gradient.Stops.Add(new GradientStop { Color = GetRandomColor() });
            Gradient.Stops.Add(new GradientStop { Color = GetRandomColor() });
        }
    }
}
