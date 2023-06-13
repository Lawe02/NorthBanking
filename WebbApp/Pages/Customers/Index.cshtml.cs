using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;

namespace WebbApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]

    public class IndexModel : PageModel
    {
        private readonly ICustomerService _cusService;

        public IndexModel(ICustomerService service)
        {
            _cusService = service;
        }
        public int PageNr { get; set; } = 1;
        public List<CustomerViewModel> Customers { get; set; }
        public string Q { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
        public void OnGet(string sortColumn, string sortOrder, string q, int pageNr)
        {
            if (pageNr != 0)
                PageNr = pageNr;
            else
                PageNr = 1;

            if (q != null)
                Q = q;

            if (sortColumn != null)
                SortColumn = sortColumn;

            if (sortOrder != null)
                SortOrder = sortOrder;

            Customers = _cusService.GetCustomers(SortColumn, SortOrder, Q, PageNr);

        }
    }
}
