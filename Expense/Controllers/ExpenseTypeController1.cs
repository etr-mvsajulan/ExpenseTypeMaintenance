using Expense.DAL;
using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public IActionResult Index(string searchs)
        {
            var ExpenseType = _context.ExpenseTypes.Where(x=> x.Description.Contains(searchs) || searchs==null).ToList();
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

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            try
            {
                var expensetype = _context.ExpenseTypes.SingleOrDefault(x => x.ExpenseTypeID == ID);
                if (expensetype != null)
                {
                    var expensetypeView = new ExpensetypeViewModel()
                    {
                        ExpenseTypeID = expensetype.ExpenseTypeID,
                        Code = expensetype.Code,
                        Description = expensetype.Description
                    };
                    return View(expensetypeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Expense type not available with the Id: {ID}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public IActionResult Edit(ExpensetypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var expensetype = new ExpenseMaintenance()
                    {
                        ExpenseTypeID = model.ExpenseTypeID,
                        Code = model.Code,
                        Description = model.Description
                    };
                    _context.ExpenseTypes.Update(expensetype);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Expense Type updated successfully!";
                    return RedirectToAction("Index");


                }
                else
                {
                    TempData["errorMessage"] = "Model data is invalid!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }           
        }

        public IActionResult Delete(int ID)
        {
            try
            {
                var expensetype = _context.ExpenseTypes.SingleOrDefault(x => x.ExpenseTypeID == ID);
                if (expensetype != null)
                {
                    var expensetypeView = new ExpensetypeViewModel()
                    {
                        ExpenseTypeID = expensetype.ExpenseTypeID,
                        Code = expensetype.Code,
                        Description = expensetype.Description
                    };
                    return View(expensetypeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Model id = {ID} is invalid!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult Delete(ExpensetypeViewModel model)
        {
            try
            {
                var expensetypeView = _context.ExpenseTypes.SingleOrDefault(x => x.ExpenseTypeID == model.ExpenseTypeID);
                if (expensetypeView != null)
                {
                    _context.ExpenseTypes.Remove(expensetypeView);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Expense type deleted successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Expense type id = {model.ExpenseTypeID}";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
                
            }

        }
    }
}
