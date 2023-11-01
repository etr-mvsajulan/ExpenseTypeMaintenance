using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.Models
{
    public class ExpenseViewModel
    {
        [JsonPropertyName("ID")]
        [Key]
        public int ExpenseId { get; set; }

        [DisplayName("Transaction Number")]
        public string TransactionNumber { get; set; }

        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("CostUnit Code")]
        public string CostUnitCode { get; set; }

        [DisplayName("CostUnit Name")]
        public string CostUnitName { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Taxable Total")]
        public decimal TaxableTotal { get; set; }

        [DisplayName("Vat Total")]
        public decimal VatTotal { get; set; }

        [DisplayName("NetOfVat Total")]
        public decimal NetOfVatTotal { get; set; }

        [DisplayName("Status")]
        public ExpenseStatus Status { get; set; }

        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Updated By")]
        public int UpdatedBy { get; set; }

        [DisplayName("Updated Date")]
        public DateTime UpdatedDate { get; set; }

        public IEnumerable<ExpenseDetailsViewModel> ExpenseDetails { get; set; }

        public enum ExpenseStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
       
    }

    public class CreateExpenseViewModel
    {
        public string TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CostUnitCode { get; set; }
        public string CostUnitName { get; set; }
        public string Remarks { get; set; }
        public decimal TaxableTotal { get; set; }
        public decimal VatTotal { get; set; }
        public decimal NetOfVatTotal { get; set; }
        public ExpenseStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public enum ExpenseStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }

    public class UpdateExpenseViewModel
    {
        public int ExpenseId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CostUnitCode { get; set; }
        public string CostUnitName { get; set; }
        public string Remarks { get; set; }
        public decimal TaxableTotal { get; set; }
        public decimal VatTotal { get; set; }
        public decimal NetOfVatTotal { get; set; }
        public ExpenseStatus Status { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public enum ExpenseStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }

}
