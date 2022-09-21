namespace InventoryManagementSystem.ViewModel
{
    public class Pager
    {
        public int TotalItems { get; private set; }//private means it's only red able 
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public Pager()
        {

        }
        public Pager(int totallItems, int page, int pageSize = 5)
        {
            int totalPages = (int)Math.Ceiling((decimal)totallItems / (decimal)pageSize);
            int currentPage = page;
            int startPage = currentPage  - 3;
            int endPage = currentPage + 2;
            if(startPage <= 0)
            {
                endPage = endPage - (StartPage - 1);
                startPage = 1;
            }
            if(endPage >= totalPages)
            {
                endPage = totalPages;
                if(endPage > 5)
                {
                    startPage = endPage - 5;
                }
            }

            TotalPages = totallItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
