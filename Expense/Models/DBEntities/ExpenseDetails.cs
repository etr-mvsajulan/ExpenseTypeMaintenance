using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Expense.Models.DBEntities
{
    public class ExpenseDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column(TypeName = "int")]
        public int ExpenseDetailID { get; set; }

        [Column(TypeName = "int")]
        public int Expenseid { get; set;}

        [Column(TypeName = "int")]
        public int ExpenseTypeID { get; set;}

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set;}

        [Column(TypeName = "varchar(150)")]
        public string Remarks { get; set;}

        [Column(TypeName = "decimal(18,4)")]
        public decimal NetOfVatAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VatAmount { get; set; }
    }
}
