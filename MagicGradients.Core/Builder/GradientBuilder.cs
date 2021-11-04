using System;
using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public class GradientBuilder : StopsBuilder<GradientBuilder>
    {
        private IChildBuilder _currentBuilder;
        private readonly List<IChildBuilder> _children = new();

        protected override GradientBuilder Instance => this;
        public override List<IGradientStop> Stops => GetCurrentBuilder().Stops;

        public GradientBuilder() : this(GlobalSetup.Current.GradientFactory)
        {
            
        }

        public GradientBuilder(IGradientFactory factory)
        {
            Factory = factory;
        }

        public GradientBuilder AddLinearGradient(Action<LinearGradientBuilder> setup = null)
        {
            var builder = new LinearGradientBuilder();
            UseBuilder(builder);

            setup?.Invoke(builder);
            return this;
        }

        public GradientBuilder AddRadialGradient(Action<RadialGradientBuilder> setup = null)
        {
            var builder = new RadialGradientBuilder();
            UseBuilder(builder);

            setup?.Invoke(builder);
            return this;
        }

        public GradientBuilder AddCssGradient(string styleSheet)
        {
            var builder = new CssGradientBuilder(styleSheet);

            UseBuilder(builder);
            return this;
        }

        public void UseBuilder(IChildBuilder builder)
        {
            _currentBuilder = builder;
            _currentBuilder.Factory = Factory;
            _children.Add(builder);
        }

        public List<IGradient> Build()
        {
            var gradients = new List<IGradient>();

            foreach (var child in _children)
            {
                child.AddConstructed(gradients);
            }

            return gradients;
        }

        internal List<IGradient> BuildReversed()
        {
            var gradients = new List<IGradient>();

            for (var i = _children.Count - 1; i >= 0; i--)
            {
                _children[i].AddConstructed(gradients);
            }

            return gradients;
        }

        private IChildBuilder GetCurrentBuilder()
        {
            if (_currentBuilder == null)
            {
                UseBuilder(new LinearGradientBuilder());
            }

            return _currentBuilder;
        }
    }
}
