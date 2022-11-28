
using LatHtaukBayDin20221120GraphQL.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LatHtaukBayDin20221120GraphQL.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogModel> blogs { get; set; }
     
    }
}
