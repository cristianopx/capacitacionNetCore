using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace Model.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<RoomUserEntity> RoomUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasNoKey();
        }

    }
}
