
using Microsoft.EntityFrameworkCore;
using studenrt.profileeian.Model;


namespace studenrt.profileeian.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<StudentInfo> Student_info { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

    }
}
