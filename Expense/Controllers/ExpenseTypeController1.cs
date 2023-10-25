using Expense.DAL;
using Expense.Models;
using Expense.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System.Security.Cryptography.Xml;

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
        public IActionResult Index(string searchs, int page = 1, int currentPage = 1, int itemsPerPage = 10)
        {

            var expenseTypeList = _expenseTypeService.GetExpensetypes(searchs, page, currentPage, itemsPerPage);


            if (page < 1)
                page = 1;

            int recsCount = expenseTypeList.Count();
            var pager = new Pager(page, currentPage, recsCount);

            int recSkip = (pager.CurrentPage - 1) * itemsPerPage;
            var data = expenseTypeList.Skip(recSkip).Take(itemsPerPage).ToList();

            // Calculate the start and end pages based on the maxPages
            

            ViewBag.Pager = pager;

            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            string sequence = _expenseTypeService.GenerateCode();
            ViewData["ExpenseSequence"] = sequence;
            return View();
        }

        public IActionResult Create(CreateExpenseTypeModel expensetypeData)
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
        public IActionResult Edit(UpdateExpenseTypeModel model)
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
