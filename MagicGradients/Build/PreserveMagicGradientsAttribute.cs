using System;
using System.ComponentModel;

namespace MagicGradients.Build
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class PreserveMagicGradientsAttribute : Attribute
    {
    }
}