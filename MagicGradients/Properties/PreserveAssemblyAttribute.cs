using System;
using System.ComponentModel;

namespace MagicGradients.Properties
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class PreserveMagicGradientsAttribute : Attribute
    {
    }
}