using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Models.DBEntities
{
    public class Expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string TransactionNumber { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string CostUnitCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string CostUnitName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Remarks { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TaxableTotal { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal VatTotal { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal NetOfVatTotal { get; set; }

        [Column(TypeName = "int")]
        public int Status { get; set; }

        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "int")]
        public int UpdatedBy { get; set; }

        [Column(TypeName = "datetime")]
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
