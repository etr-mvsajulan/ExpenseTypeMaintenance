using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Expense.DAL
{
    public class ExpenseDBContext : IdentityDbContext<UserRegistration>
    {
        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options) : base(options) { }
        public DbSet<Expense.Models.DBEntities.Expense> Expense { get; set; }
        public DbSet<Expense.Models.DBEntities.ExpenseDetails> ExpenseDetails { get; set; }
        public DbSet<ExpenseTypes> ExpenseTypes { get; set; }
        public DbSet<UserRegistration> UserRegistration { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence("MySequence");

            // Configure the primary key for IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });
        }
    }
}
