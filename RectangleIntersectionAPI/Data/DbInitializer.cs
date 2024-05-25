

using RectangleIntersectionAPI.Data.Entities;

namespace RectangleIntersectionAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Rectangles.Any())
            {
                return; // DB has been seeded
            }

            var rectangles = new Rectangle[]
            {
            new Rectangle {Id=1, X = 0, Y = 0, Width = 5, Height = 5 },
            new Rectangle {Id=2, X = 10, Y = 10, Width = 3, Height = 3 },
                // Add more rectangles as needed
            };

            context.Rectangles.AddRange(rectangles);
            context.SaveChanges();
        }
    }
}
