using Expense.DAL;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Expense.Controllers
{
    [Route("api/expenses/types/v1")]
    [ApiController]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypesController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

      

        // GET: api/expenses/types/v1/list
        [HttpGet]
        public JsonResult List()
        {
            var sortedExpenseTypes = _expenseTypeService.GetExpensetypes("", 1,1,10);
            return new JsonResult(sortedExpenseTypes);
        }
    }
}

