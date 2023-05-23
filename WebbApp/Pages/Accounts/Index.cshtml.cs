using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;

namespace WebbApp.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _service;
        public IndexModel(IAccountService accService)
        {
            _service = accService;
        }
        public int PageNr { get; set; } = 1;
        public List<AccountViewModel> Accounts { get; set; }
        public string Q { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public void OnGet(string sortColumn, string sortOrder, int pageNr, string q)
        {
            if (pageNr != 0)
                PageNr = pageNr;
            else
                PageNr = 1;

            if (sortColumn != null)
                SortColumn = sortColumn;

            if (sortOrder != null)
                SortOrder = sortOrder;

            if (q != null)
                Q = q;

            Accounts = _service.GetAccounts(SortColumn, SortOrder, PageNr, Q);
        }
    }
}
