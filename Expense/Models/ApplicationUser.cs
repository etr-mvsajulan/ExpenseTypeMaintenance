using Microsoft.AspNetCore.Identity;

namespace Expense.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CostUnitCode { get; set; }
        public string CostUnitName { get; set; }
    }
}
