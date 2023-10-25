using Expense.Models.DBEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Expense.DAL
{ 
    public class ExpenseTypeDBContext : DbContext
    {
        public ExpenseTypeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExpenseTypes> ExpenseTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("MySequence");
        }
        
    }
}
