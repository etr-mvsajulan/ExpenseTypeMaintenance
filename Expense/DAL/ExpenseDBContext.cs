using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Expense.Models;

namespace Expense.DAL
{
    public class ExpenseDBContext : DbContext
    {
        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options) : base(options) { }
        public DbSet<Expense.Models.DBEntities.Expense> Expense { get; set; }
        public DbSet<Expense.Models.DBEntities.ExpenseDetails> ExpenseDetails { get; set; }
    }
}
