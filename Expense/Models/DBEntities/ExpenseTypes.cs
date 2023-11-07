using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Models.DBEntities
{
    public class ExpenseTypes
    {
        [Key]
        public int ExpenseTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
