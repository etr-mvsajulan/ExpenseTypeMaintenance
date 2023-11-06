using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Expense.Models.DBEntities
{
    public class UserRegistration 
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CostUnitCode { get; set; }
        public string CostUnitName { get; set; }
        public string Email { get; set; }
        public string Source { get; set; }  
    }
}
