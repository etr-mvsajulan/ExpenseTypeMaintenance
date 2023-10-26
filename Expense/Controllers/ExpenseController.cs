using Microsoft.AspNetCore.Mvc;

namespace Expense.Controllers
{
    public class ExpenseController : Controller
    {
        [Route("/Index")]
        public IActionResult Submit()
        {
            return View("~/Views/Expense/Index.cshtml");
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Create()
        {
            
            return View();
        }

        public IActionResult Edit()
        {
            
            return View();
        }

        public IActionResult Delete()
        {
            
            return View();
        }
        public IActionResult Details()
        {
            
            return View();
        }
    }
}
