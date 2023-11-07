using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Expense.Models.DBEntities
{
    public class ExpenseDetails
    {
        [Key]
        public int ExpenseDetailID { get; set; }

        public int Expenseid { get; set;}

        public int ExpenseTypeID { get; set;}

        public decimal Amount { get; set;}

        public string Remarks { get; set;}

        public decimal NetOfVatAmount { get; set; }

        public decimal VatAmount { get; set; }
    }
}
