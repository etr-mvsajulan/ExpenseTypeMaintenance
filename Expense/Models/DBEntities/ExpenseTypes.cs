using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Models.DBEntities
{
    public class ExpenseTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseTypeID { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string Code { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string Description { get; set; }
    }
}
