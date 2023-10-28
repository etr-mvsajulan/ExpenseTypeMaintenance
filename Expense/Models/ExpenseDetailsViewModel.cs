using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Expense.Models
{
    public class ExpenseDetailsViewModel
    {
        public int ExpenseDetailID { get; set; }

        public int Expenseid { get; set; }

        [DisplayName("Expense Type")]
        public int ExpenseTypeID { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }
    }
}
