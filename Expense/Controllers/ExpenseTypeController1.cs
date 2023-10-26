using Expense.DAL;
using Expense.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System.Security.Cryptography.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IActionResult Index(string searchs, int page = 1, int currentPage = 1, int itemsPerPage = 5)
        {

            var expenseTypeList = _expenseTypeService.GetExpensetypes(searchs, page, currentPage, itemsPerPage);

            int skip = (page - 1) * itemsPerPage;
            var itemsOnPage = expenseTypeList.Skip(skip).Take(itemsPerPage).ToList();
            int totalPages = (int)Math.Ceiling((double)expenseTypeList.Count() / itemsPerPage);

            // Pass the items, search term, and pagination information to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchs;
            ViewBag.ItemsPerPage = itemsPerPage;

            // Calculate "First" and "Last" pages
            ViewBag.FirstPage = 1;
            ViewBag.LastPage = totalPages;
            return View(itemsOnPage);
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
