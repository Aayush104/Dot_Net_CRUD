using DotNetCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCrud.Services
{
    public class ApplicationDbContext : DbContext

        
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
