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

        [DisplayName("TransactionNumber")]
        public string TransactionNumber { get; set; }

        [DisplayName("TransactionDate")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("CostUnitCode")]
        public string CostUnitCode { get; set; }

        [DisplayName("CostUnitName")]
        public string CostUnitName { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("TaxableTotal")]
        public decimal TaxableTotal { get; set; }

        [DisplayName("VatTotal")]
        public decimal VatTotal { get; set; }

        [DisplayName("NetOfVatTotal")]
        public decimal NetOfVatTotal { get; set; }

        [DisplayName("Status")]
        public int Status { get; set; }

        [DisplayName("CreatedBy")]
        public int CreatedBy { get; set; }

        [DisplayName("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("UpdatedBy")]
        public int UpdatedBy { get; set; }

        [DisplayName("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [NotMapped] // Not mapped to the database
        public ExpenseStatus StatusEnum
        {
            get { return (ExpenseStatus)Status; }
            set { Status = (int)value; }
        }

        public enum ExpenseStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }

    [Keyless]
    public class CreateExpenseModel
    {
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

        [NotMapped] // Not mapped to the database
        public ExpenseStatus StatusEnum
        {
            get { return (ExpenseStatus)Status; }
            set { Status = (int)value; }
        }

        public enum ExpenseStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }

    
    public class UpdateExpenseModel
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

        [NotMapped] // Not mapped to the database
        public ExpenseStatus StatusEnum
        {
            get { return (ExpenseStatus)Status; }
            set { Status = (int)value; }
        }

        public enum ExpenseStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }
}
