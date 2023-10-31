using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.Models.DBEntities
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string TransactionNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string CostUnitCode { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CostUnitName { get; set; }

        [Column(TypeName = "varchar(150)")]
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

        public ICollection<ExpenseDetailsViewModel> ExpenseDetails { get; set; }
    }
}
