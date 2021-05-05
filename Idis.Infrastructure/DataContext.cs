using Microsoft.EntityFrameworkCore;

namespace Idis.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Intern> Interns { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}