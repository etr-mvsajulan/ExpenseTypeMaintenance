using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.Models.DBEntities
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        public string TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public string CostUnitCode { get; set; }

        public string CostUnitName { get; set; }

        public string Remarks { get; set; }

        public decimal TaxableTotal { get; set; }

        public decimal VatTotal { get; set; }

        public decimal NetOfVatTotal { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<ExpenseDetailsViewModel> ExpenseDetails { get; set; }
    }
}
