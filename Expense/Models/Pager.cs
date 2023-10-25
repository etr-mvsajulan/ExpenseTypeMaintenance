using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set;}
        public int PageSize { get; set;}

        public int TotalPages { get; set;}
        public int StartPage { get; set;}
        public int EndPage { get; set;}
        public int MaxPages { get; set; }

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
