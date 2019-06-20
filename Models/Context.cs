using Microsoft.EntityFrameworkCore;

namespace secrets.Models
{
    public class secretsContext : DbContext
    {
        public secretsContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Secret> Secrets { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}