using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace Expense.Models
{
    public class ExpenseDetailsViewModel
    {
        [JsonPropertyName("ID")]
        [Key]
        public int ExpenseDetailID { get; set; }

        public int Expenseid { get; set; }

        [DisplayName("Expense Type")]
        public int ExpenseTypeID { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Net Of VatAmount")]
        public decimal NetOfVatAmount { get; set; }

        [DisplayName("Vat Amount")]
        public decimal VatAmount { get; set; }

        [DisplayName("Expense Description")]
        public string Description { get; set; }

        public bool isShow {  get; set; }
    }

    public class CreateExpenseDetailsViewModel
    {
        public int Expenseid { get; set; }
        public int ExpenseTypeID { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public decimal NetOfVatAmount { get; set; }
        public decimal VatAmount { get; set; }
    }

    public class UpdateExpenseDetailsViewModel
    {
        public int ExpenseDetailID { get; set; }
        public int Expenseid { get; set; }
        public int ExpenseTypeID { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public decimal NetOfVatAmount { get; set; }
        public decimal VatAmount { get; set; }
    }
}
