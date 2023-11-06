using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Expense.Models
{
    public class UserRegistrationViewModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Last Name")]
        public string Email { get; set; }

        [DisplayName("CostUnit Code")]
        public string CostUnitCode { get; set; }

        [DisplayName("CostUnit Name")]
        public string CostUnitName { get; set; }

        [DisplayName("Source")]
        public string Source { get; set; }
    }
}
