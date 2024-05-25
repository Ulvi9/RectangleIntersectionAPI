using Microsoft.EntityFrameworkCore;
using RectangleIntersectionAPI.Data.Entities;
using System.Collections.Generic;

namespace RectangleIntersectionAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Rectangle> Rectangles { get; set; }
    }
}
