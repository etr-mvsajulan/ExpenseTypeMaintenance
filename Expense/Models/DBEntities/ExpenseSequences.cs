using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Models.DBEntities
{
    public class ExpenseSequences
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column(TypeName = "int")]
        public int Sequences { get; set; }
    }
}
