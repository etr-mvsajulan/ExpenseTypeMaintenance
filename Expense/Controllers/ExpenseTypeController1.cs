using Expense.DAL;
using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Expense.Controllers
{
    public class ExpenseTypeController1 : Controller
    {
        private readonly ExpenseTypeDBContext _context;
        public ExpenseTypeController1(ExpenseTypeDBContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var ExpenseType = _context.ExpenseTypes.ToList();
            List<ExpensetypeViewModel> expensetypeList = new List<ExpensetypeViewModel>();
            if (ExpenseType != null) 
            {
                
                foreach (var ExpenseT in ExpenseType)
                {
                    var ExpensetypeViewModel = new ExpensetypeViewModel()
                    {
                        ExpenseTypeID = ExpenseT.ExpenseTypeID,
                        Code = ExpenseT.Code,
                        Description = ExpenseT.Description
                    };
                    expensetypeList.Add(ExpensetypeViewModel);
                }
                return View(expensetypeList);
            }
            return View(expensetypeList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(ExpensetypeViewModel expensetypeData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var expensetype = new ExpenseMaintenance
                    {
                        Code = expensetypeData.Code,
                        Description = expensetypeData.Description
                    };

                    _context.ExpenseTypes.Add(expensetype);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Expense type created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                }
                return View();
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
