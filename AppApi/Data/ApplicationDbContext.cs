using AppApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    };
}
