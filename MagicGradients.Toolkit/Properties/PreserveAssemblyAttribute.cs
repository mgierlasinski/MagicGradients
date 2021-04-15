using System;
using System.ComponentModel;

namespace MagicGradients.Toolkit.Properties
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class PreserveAssemblyAttribute : Attribute
    {
    }
}