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
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypeController1(IExpenseTypeService expenseTypeService)
        {
            this._expenseTypeService = expenseTypeService;
        }
        [HttpGet]
        public IActionResult Index(string searchs, int pg = 1)
        {         

            var expenseTypeList = _expenseTypeService.GetExpensetypes(searchs);

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = expenseTypeList.Count();
            var pager = new Pager(pg, pageSize, recsCount);
            int recSkip = (pg - 1) * pageSize;
            var data = expenseTypeList.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
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
                    _expenseTypeService.CreateExpenseType(expensetypeData);
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
                var expensetypeView = _expenseTypeService.GetExpenseTypeById(ID);
                if (expensetypeView != null)
                {                    
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
                    _expenseTypeService.UpdateExpenseType(model);
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
                var expenseTypeView = _expenseTypeService.GetExpenseTypeById(ID);
                if (expenseTypeView != null)
                {                   
                    return View(expenseTypeView);
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
                if (model != null)
                {
                    _expenseTypeService.DeleteExpenseType(model.ExpenseTypeID);
                    TempData["successMessage"] = "Expense type deleted successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Expense type id = {model?.ExpenseTypeID}";
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
