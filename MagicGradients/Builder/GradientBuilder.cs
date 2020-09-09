﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients.Builder
{
    public class GradientBuilder : StopsBuilder<GradientBuilder>
    {
        private IChildBuilder _currentBuilder;
        private readonly List<IChildBuilder> _children = new List<IChildBuilder>();

        protected override GradientBuilder Instance => this;
        public override StopsFactory StopsFactory => GetCurrentBuilder().StopsFactory;

        public GradientBuilder AddLinearGradient(Action<LinearGradientBuilder> setup = null)
        {
            var builder = new LinearGradientBuilder();
            setup?.Invoke(builder);

            UseBuilder(builder);
            return this;
        }

        public LinearGradientBuilder AddLinearGradient(double angle, bool isRepeating = false)
        {
            var builder = new LinearGradientBuilder
            {
                Angle = angle,
                IsRepeating = isRepeating
            };
            UseBuilder(builder);
            return builder;
        }

        public GradientBuilder AddRadialGradient(Action<RadialGradientBuilder> setup = null)
        {
            var builder = new RadialGradientBuilder();
            setup?.Invoke(builder);

            UseBuilder(builder);
            return this;
        }

        public RadialGradientBuilder AddRadialGradient(
            Point center, 
            RadialGradientShape shape, 
            RadialGradientSize size, 
            RadialGradientFlags flags = RadialGradientFlags.PositionProportional, 
            bool isRepeating = false)
        {
            var builder = new RadialGradientBuilder
            {
                Center = center,
                Shape = shape, 
                Size = size, 
                Flags = flags,
                IsRepeating = isRepeating
            };

            UseBuilder(builder);
            return builder;
        }

        public Gradient[] Build()
        {
            return _children.Select(x => x.Construct()).ToArray();
        }

        private IChildBuilder GetCurrentBuilder()
        {
            if (_currentBuilder == null)
            {
                UseBuilder(new LinearGradientBuilder());
            }

            return _currentBuilder;
        }

        private void UseBuilder(IChildBuilder builder)
        {
            _currentBuilder = builder;
            _children.Add(builder);
        }
    }
}
