using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense.DAL;
using Expense.Models.DBEntities;
using Expense.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.Http;
using System.Security.Claims;

namespace Expense.Controllers
{

    [ServiceFilter(typeof(AuthorizeActionFilter))]
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            this._expenseService = expenseService;
        }

        // GET: Expenses
        public async Task<IActionResult> Index(string searchExpense, int page = 1, int currentPage = 1, int itemsPerPage = 5)
        {
            var code = User.FindFirst("CostUnitCode")?.Value;
            var expense = await Task.Run(() => _expenseService.GetExpenseList(searchExpense, costunitcode: Convert.ToString(code)));
            int skip = (page - 1) * itemsPerPage;
            var itemsOnPage = expense.Skip(skip).Take(itemsPerPage).ToList();
            int totalPages = (int)Math.Ceiling((double)expense.Count() / itemsPerPage);

            // Pass the items, search term, and pagination information to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchExpense;
            ViewBag.ItemsPerPage = itemsPerPage;

            // Calculate "First" and "Last" pages
            ViewBag.FirstPage = 1;
            ViewBag.LastPage = totalPages;

            return itemsOnPage != null ? View(itemsOnPage) : Problem("Entity set 'ExpenseDBContext.Expense'  is null.");
            
        }
               

        // GET: Expenses/Details/5
        public async Task <IActionResult> Details(int id)
        {
            var expenseDetails = await Task.Run(() => _expenseService.GetExpenseByID(id));
            if (expenseDetails == null)
            {
                return NotFound();
            }
            TempData["showColumn"] = true;
            return View(expenseDetails);
        }

        // GET: Expenses/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Expenses/Create
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExpenseViewModel expense)
        {
            int ExpenseID = -1;
            if (ModelState.IsValid)
            {
                ExpenseID = await _expenseService.CreateExpenseAsync(expense);
                return RedirectToAction("Edit", "Expenses", new { id = ExpenseID });
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var existingExpense = await Task.Run(() => _expenseService.GetExpenseByID(id));

            if (existingExpense != null) 
            {
                return View(existingExpense);
            }
            else
            {
                RedirectToAction(nameof(Index));
            }
            return View(existingExpense);
        }

        // POST: Expenses/Edit/5

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateExpenseViewModel expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Task.Run(() => _expenseService.UpdateExpenseAsync(expense));                   
                    return RedirectToAction("Edit", "Expenses", new { id = expense.ExpenseId });
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var expenseDelete = await Task.Run(() => _expenseService.GetExpenseByID(id));
                if (expenseDelete != null)
                {
                    return View(expenseDelete);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        // POST: Expenses/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ExpenseViewModel expense)
        {
            try
            {
                if (expense != null)
                {
                    await Task.Run(() =>_expenseService.DeleteExpenseAsync(expense.ExpenseId));
                    return RedirectToAction("Index");

                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);

            }
        }

        public async Task<PartialViewResult> LoadExpenseDetails(int expenseId)
        {
            IEnumerable<ExpenseDetailsViewModel> expenseDetails = await _expenseService.GetExpenseDetailsList(expenseId);
            return PartialView("_ExpenseDetails", expenseDetails);
        }

    }
}
