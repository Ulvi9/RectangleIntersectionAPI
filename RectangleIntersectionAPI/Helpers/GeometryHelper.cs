using RectangleIntersectionAPI.Data.Entities;

namespace RectangleIntersectionAPI.Helpers
{
    public static class GeometryHelper
    {
        public static bool Intersects(this Rectangle rect, Segment segment)
        {
            // Check if the segment intersects any of the rectangle's edges
            return SegmentIntersects(segment, rect.X, rect.Y, rect.X2, rect.Y) || // Top edge
                   SegmentIntersects(segment, rect.X, rect.Y, rect.X, rect.Y2) || // Left edge
                   SegmentIntersects(segment, rect.X2, rect.Y, rect.X2, rect.Y2) || // Right edge
                   SegmentIntersects(segment, rect.X, rect.Y2, rect.X2, rect.Y2); // Bottom edge
        }

        private static bool SegmentIntersects(Segment segment, double x1, double y1, double x2, double y2)
        {
            double d = (segment.X2 - segment.X1) * (y2 - y1) - (segment.Y2 - segment.Y1) * (x2 - x1);
            if (d == 0) return false; // Parallel

            double u1 = ((x1 - segment.X1) * (y2 - y1) - (y1 - segment.Y1) * (x2 - x1)) / d;
            double u2 = ((x1 - segment.X1) * (segment.Y2 - segment.Y1) - (y1 - segment.Y1) * (segment.X2 - segment.X1)) / d;

            return (u1 >= 0 && u1 <= 1) && (u2 >= 0 && u2 <= 1);
        }
    }
}
