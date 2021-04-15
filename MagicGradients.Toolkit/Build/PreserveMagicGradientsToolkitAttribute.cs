using System;
using System.ComponentModel;

namespace MagicGradients.Toolkit.Build
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class PreserveMagicGradientsToolkitAttribute : Attribute
    {
    }
}