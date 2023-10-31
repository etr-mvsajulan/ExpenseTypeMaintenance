using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.Models
{
    public class ExpensetypeViewModel
    {
        [JsonPropertyName("ID")]
        [Key]
        public int ExpenseTypeID { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }

    public class CreateExpenseTypeModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class UpdateExpenseTypeModel
    {
        public int ExpenseTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
