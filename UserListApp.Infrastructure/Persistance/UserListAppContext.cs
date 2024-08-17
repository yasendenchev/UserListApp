using Microsoft.EntityFrameworkCore;
using UserListApp.Infrastructure.Entities;

namespace UserListApp.Infrastructure.Persistance
{
    public class UserListAppContext : DbContext
    {
        public UserListAppContext(DbContextOptions<UserListAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(DataSeed.Users);
        }

        public DbSet<User> Users { get; set; }
    }
}
