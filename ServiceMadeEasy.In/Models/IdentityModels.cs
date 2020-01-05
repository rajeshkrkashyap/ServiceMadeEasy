using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServiceMadeEasy.In.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser
    {
        //public string UserName { get; set; }
        //public string Email { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            if (!Database.Exists())
            {
                Database.SetInitializer(new DBInitializer());
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            //modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Sponsor>().ToTable("Sponsor");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("ApplicationUserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("ApplicationUserLogin");
            modelBuilder.Entity<ImageResource>().ToTable("ImageResource");

            modelBuilder.Entity<Package>().ToTable("Package");


        }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<IdentityUserRole> ApplicationUserRoles { get; set; }
        public virtual DbSet<IdentityUserClaim> ApplicationUserClaims { get; set; }
        public virtual DbSet<IdentityUserLogin> ApplicationUserLogins { get; set; }
        public virtual DbSet<ImageResource> ImageResources { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
    }
}