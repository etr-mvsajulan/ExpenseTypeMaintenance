using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set;}
        public int PageSize { get; private set;}

        public int TotalPages { get; private set;}
        public int StartPage { get; private set;}
        public int EndPage { get; private set;}

        public Pager()
        {

        }

        public Pager(int Page, int CPage, int ItemsPerPage)
        {
            int totalPages = (int)Math.Ceiling((decimal)ItemsPerPage / (decimal)Page);
            int currentPage = CPage;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            } 

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = ItemsPerPage;
            CurrentPage = currentPage;
            PageSize = Page;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        
    }
}
