using System;
using System.Collections.Generic;
using System.Linq;

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
            setup?.Invoke(builder);

            UseBuilder(builder);
            return this;
        }

        public GradientBuilder AddRadialGradient(Action<RadialGradientBuilder> setup = null)
        {
            var builder = new RadialGradientBuilder();
            setup?.Invoke(builder);

            UseBuilder(builder);
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

        public IEnumerable<IGradient> Build()
        {
            return _children.Select(x => x.Construct());
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
