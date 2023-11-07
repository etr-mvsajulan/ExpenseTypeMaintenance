using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Expense.DAL
{
    public class AuthorizeActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("/Identity/Account/Register");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing after the action is executed
        }
    }
}
