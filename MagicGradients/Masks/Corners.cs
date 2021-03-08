namespace MagicGradients.Masks
{
    public struct Corners
    {
        public Dimensions TopLeft { get; set; }
        public Dimensions TopRight { get; set; }
        public Dimensions BottomLeft { get; set; }
        public Dimensions BottomRight { get; set; }

        public Corners(Dimensions uniformSize) 
            : this(uniformSize, uniformSize, uniformSize, uniformSize)
        {
        }

        public Corners(Dimensions topSize, Dimensions bottomSize) 
            : this(topSize, topSize, bottomSize, bottomSize)
        {
        }

        public Corners(Dimensions topLeft, Dimensions topRight, Dimensions bottomRight, Dimensions bottomLeft) 
            : this()
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
}
