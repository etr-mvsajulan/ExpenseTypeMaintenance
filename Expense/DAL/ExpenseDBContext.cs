using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Expense.DAL
{
    public class ExpenseDBContext : IdentityDbContext<ApplicationUser>
    {

        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options) : base(options) { }

        public DbSet<Expense.Models.DBEntities.Expense> Expense { get; set; }
        public DbSet<ExpenseDetails> ExpenseDetails { get; set; }
        public DbSet<ExpenseTypes> ExpenseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            builder.Entity<ApplicationUser>(
                entity =>
                    {
                        entity.ToTable(name: "Users");
                    }
                );
            builder.Entity<IdentityRole>(
                entity =>
                    {
                        entity.ToTable(name: "Roles");
                    }
                );
            builder.Entity<IdentityUserRole<string>>(
                entity =>
                    {
                        entity.ToTable(name: "UserRoles");
                    }
                );
            builder.Entity<IdentityUserLogin<string>>(
                entity =>
                    {
                        entity.ToTable(name: "UserLogins");
                    }
                );
            builder.Entity<IdentityUserClaim<string>>(
                entity =>
                    {
                        entity.ToTable(name: "UserClaims");
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

        
    }
}
