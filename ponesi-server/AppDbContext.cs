using Microsoft.EntityFrameworkCore;
using PonesiWebApi.Models;
using System.Reflection.Metadata;

namespace PonesiWebApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
    }
}
