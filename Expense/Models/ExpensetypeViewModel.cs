using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Models
{
    public class ExpensetypeViewModel
    {
        public int ExpenseTypeID { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }




    }
}
