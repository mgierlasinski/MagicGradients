using Xamarin.Forms;

namespace MagicGradients.Animation
{
    [TypeConverter(typeof(RepeatBehaviorTypeConverter))]
    public struct RepeatBehavior
    {
        public RepeatBehavior(RepeatBehaviorType type, int count)
        {
            Type = type;
            Count = count;
        }
        
        public RepeatBehaviorType Type { get; set; }
        public int Count { get; set; }
    }

    public enum RepeatBehaviorType
    {
        Count, Forever
    }
}
