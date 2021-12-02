//using Microsoft.Maui.Graphics;
//using System.Collections.Generic;

//namespace MagicGradients.Builder
//{
//    public class GradientFactory : IGradientFactory
//    {
//        public ILinearGradient Construct(LinearGradientBuilder builder)
//        {
//            return new LinearGradient
//            {
//                Angle = builder.Angle,
//                IsRepeating = builder.IsRepeating,
//                Stops = new List<IGradientStop>(builder.Stops)
//            };
//        }

//        public IRadialGradient Construct(RadialGradientBuilder builder)
//        {
//            return new RadialGradient
//            {
//                Center = builder.Center,
//                Shape = builder.Shape,
//                Size = builder.Size,
//                RadiusX = builder.RadiusX,
//                RadiusY = builder.RadiusY,
//                Flags = builder.Flags,
//                IsRepeating = builder.IsRepeating,
//                Stops = new List<IGradientStop>(builder.Stops)
//            };
//        }

//        public IGradientStop CreateStop(Color color, Offset? offset = null)
//        {
//            return new GradientStop
//            {
//                Color = color,
//                Offset = offset ?? Offset.Empty
//            };
//        }
//    }
//}
