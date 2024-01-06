using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static RembraceLogIn.Shared.Models.AccountModel;

namespace RembraceLogIn.Server.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            });

			builder.Entity<IdentityRole>().HasData(new IdentityRole
			{
				Name = "Admin",
				NormalizedName = "ADMIN",
				Id = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString(),
			});

            //builder.Entity<ApplicationUser>()
            //.HasMany(u => u.Accounts)
            //.WithOne(a => a.User)
            //.HasForeignKey(a => a.UserId);

        } 
    }
}
