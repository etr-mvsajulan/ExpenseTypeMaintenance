using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Expense.DAL
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");
            builder.Entity<ApplicationUser>(
                    entity =>
                    {
                        entity.ToTable(name: "User");
                    }
                );
            builder.Entity<IdentityRole>(
                   entity =>
                   {
                       entity.ToTable(name: "Role");
                   }
               );
            builder.Entity<IdentityUserRole<string>>(
                    entity =>
                    {
                        entity.ToTable(name: "UserRole");
                    }
                );
            builder.Entity<IdentityUserClaim<string>>(
                    entity => {
                        entity.ToTable(name: "UserClaims");
                    }
                );
            builder.Entity<IdentityUserLogin<string>>(
                    entity =>
                    {
                        entity.ToTable(name: "UserLogins");
                    } 
                );
            builder.Entity<IdentityRoleClaim<string>>(
                    entity =>
                    {
                        entity.ToTable(name: "RoleClaims");
                    }
                );
            builder.Entity<IdentityUserToken<string>>(
                    entity =>
                    {
                        entity.ToTable(name: "UserTokens");
                    }
                );
        } 

        public DbSet<UserRegistration> UserRegistration { get; set; }
    }
}
