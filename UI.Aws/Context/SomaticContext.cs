using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UI.Aws.Context
{
    public class SomaticContext : DbContext
    {
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Sequence> Sequence { get; set; }
        public DbSet<Leaner> Leaners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MediaUrl> MediaUrl { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


        public SomaticContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>().HasMany(e => e.Sequences).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Workout>().HasOne(e => e.CreatedBy).WithMany(e => e.CreatedWorkouts).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Exercise>().HasMany(e => e.MediaUrls).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Exercise>().HasMany(e => e.Sequences).WithOne(e => e.Exercise).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Machine>().HasMany(e => e.MediaUrls).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Machine>().HasMany(e => e.Exercises).WithOne(e => e.Machine).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Teacher>().HasMany(e => e.CreatedWorkouts).WithOne(e => e.CreatedBy).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
