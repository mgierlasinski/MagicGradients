using MagicGradients.Converters;
using System;
using System.ComponentModel;
using System.Globalization;

namespace MagicGradients
{
    [TypeConverter(typeof(OffsetTypeConverter))]
    public struct Offset
    {
        public static Offset Empty { get; } = new Offset(-1, OffsetType.Proportional);
        public static Offset Zero { get; } = new Offset(0, OffsetType.Proportional);

        public double Value { get; set; }
        public OffsetType Type { get; set; }

        public bool IsEmpty => Value < 0;

        public Offset(double value, OffsetType type)
        {
            Value = value;
            Type = type;
        }

        public static Offset Prop(double value) => new Offset(value, OffsetType.Proportional);
        public static Offset Abs(double value) => new Offset(value, OffsetType.Absolute);

        public static bool operator ==(Offset o1, Offset o2) => o1.Value == o2.Value;
        public static bool operator !=(Offset o1, Offset o2) => o1.Value != o2.Value;

        public static Offset Parse(string value, OffsetType defaultType)
        {
            if (TryParse(value, defaultType, out var result))
                return result;
                
            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Offset)}");
        }

        public static bool TryParse(string value, OffsetType defaultType, out Offset result)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim();

                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
                {
                    result = new Offset(d, defaultType);
                    return true;
                }

                if (TryParseWithUnit(value, out var res))
                {
                    result = res;
                    return true;
                }
            }

            result = Empty;
            return false;
        }

        public static bool TryParseWithUnit(string token, out Offset result)
        {
            if (token != null)
            {
                if (token.TryExtractNumber("%", out var percent))
                {
                    var value = Math.Min(percent / 100, 1f); // No bigger than 1
                    result = new Offset(value, OffsetType.Proportional);
                    return true;
                }

                if (token.TryExtractNumber("px", out var pixels))
                {
                    result = new Offset(pixels, OffsetType.Absolute);
                    return true;
                }
            }

            result = Zero;
            return false;
        }

        public string ToStringWithUnit()
        {
            if (IsEmpty)
                return string.Empty;

            var value = Type == OffsetType.Proportional ? Value * 100 : Value;
            var unit = Type == OffsetType.Proportional ? "%" : "px";

            return $"{value.ToString(CultureInfo.InvariantCulture)}{unit}";
        }
    }

    public enum OffsetType
    {
        Proportional,
        Absolute
    }

    public static class OffsetExtensions
    {
        public static float GetDrawPixels(this Offset offset, float sizeInPixels, float pixelScaling)
        {
            return offset.Type == OffsetType.Proportional
                ? (float)(offset.Value * sizeInPixels)
                : (float)(offset.Value * pixelScaling);
        }
    }
}
