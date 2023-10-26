using Expense.Models.DBEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Expense.Models;

namespace Expense.DAL
{
    public class ExpenseDBContext : DbContext
    {
        public ExpenseDBContext(DbContextOptions options) : base(options) 
        { 
        }
        public DbSet<Expense.Models.ExpenseViewModel> ExpenseViewModel { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("ExpenseTransactions");
        }

        
    }
}
