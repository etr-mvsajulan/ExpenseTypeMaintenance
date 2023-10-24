using Expense.Models.DBEntities;
using Microsoft.EntityFrameworkCore;
namespace Expense.DAL
{
    public class ExpenseTypeDBContext : DbContext
    {
        public ExpenseTypeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExpenseMaintenance> ExpenseTypes { get; set; }
    }
}
