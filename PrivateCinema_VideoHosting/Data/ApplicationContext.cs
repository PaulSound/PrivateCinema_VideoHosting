using Microsoft.EntityFrameworkCore;

namespace PrivateCinema_VideoHosting.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<SignIn> _signInList { get; set; } = null!;
        public DbSet<User> _userList { get; set; } = null!;
        public DbSet<Video> _videoList { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(s => s.SignIn).WithOne(s => s.User).HasForeignKey<User>(s => s.SignInId);
            modelBuilder.Entity<Video>().HasOne(l => l.User).WithMany(m => m.videoCollection).HasForeignKey(x => x.userId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SignIn>().Property(x => x.Login).HasMaxLength(30);
            modelBuilder.Entity<SignIn>().Property(x => x.Password).HasMaxLength(200);
        }
    }
}
