namespace MagicGradients.Masks
{
    public struct Corners
    {
        public Dimensions LeftTop { get; set; }
        public Dimensions LeftBottom { get; set; }
        public Dimensions RightTop { get; set; }
        public Dimensions RightBottom { get; set; }

        public Corners(Dimensions uniformSize) 
            : this(uniformSize, uniformSize, uniformSize, uniformSize)
        {
        }

        public Corners(Dimensions topSize, Dimensions bottomSize) 
            : this(topSize, topSize, bottomSize, bottomSize)
        {
        }

        public Corners(Dimensions leftTop, Dimensions rightTop, Dimensions rightBottom, Dimensions leftBottom) 
            : this()
        {
            LeftTop = leftTop;
            LeftBottom = leftBottom;
            RightTop = rightTop;
            RightBottom = rightBottom;
        }
    }
}
