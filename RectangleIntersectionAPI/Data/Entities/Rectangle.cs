﻿namespace RectangleIntersectionAPI.Data.Entities
{
    public class Rectangle
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double X2 => X + Width;
        public double Y2 => Y + Height;
    }
}
