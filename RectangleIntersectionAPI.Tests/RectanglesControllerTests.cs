

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RectangleIntersectionAPI.Controllers;
using RectangleIntersectionAPI.Data;
using RectangleIntersectionAPI.Data.Entities;

namespace RectangleIntersectionAPI.Tests
{
    public class RectanglesControllerTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public RectanglesControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RectangleTestDb")
                .Options;
        }

        private async Task SeedDatabase(ApplicationDbContext context)
        {
            context.Rectangles.AddRange(
                new Rectangle { Id = 1, X = 0, Y = 0, Width = 5, Height = 5 },
                new Rectangle { Id = 2, X = 10, Y = 10, Width = 3, Height = 3 },
                new Rectangle { Id = 3, X = 3, Y = 3, Width = 2, Height = 2 }
            );
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetIntersectingRectangles_ReturnsIntersectingRectangles()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                await SeedDatabase(context);

                var controller = new RectanglesController(context);

                // Act
                var segment = new Segment();
                segment.X1 = 1.0;
                segment.X2 = 1.0;
                segment.Y1 = 6.0;
                segment.Y2 = 1.0;
                var result = await controller.GetIntersectingRectangles(segment);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var rectangles = Assert.IsType<List<Rectangle>>(okResult.Value);
                Assert.Single(rectangles);
                Assert.Equal(1, rectangles.First().Id);
            }
        }

        [Fact]
        public async Task GetIntersectingRectangles_ReturnsEmptyListWhenNoIntersection()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                await SeedDatabase(context);

                var controller = new RectanglesController(context);
                var segment = new Segment();
                segment.X1 = 20.0;
                segment.X2 = 20.0;
                segment.Y1 = 25.0;
                segment.Y2 = 25.0;

                // Act
                var result = await controller.GetIntersectingRectangles(segment);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var rectangles = Assert.IsType<List<Rectangle>>(okResult.Value);
                Assert.Empty(rectangles);
            }
        }
    }
}
