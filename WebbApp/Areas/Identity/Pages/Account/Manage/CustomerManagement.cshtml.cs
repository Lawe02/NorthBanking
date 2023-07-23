using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;

namespace WebbApp.Areas.Identity.Pages.Account.Manage
{
    [BindProperties]
    public class CustomerManagementModel : PageModel
    {
        private readonly ICustomerService _service;
        public CustomerManagementModel(ICustomerService service)
        {
            _service = service;
        }
        
        public int PageNr { get; set; } = 1;
        public string Q { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<CustomerViewModel> Customers { get; set; }
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
            Customers = _service.GetCustomers(SortColumn, SortOrder, Q, PageNr);
        }
    }
}
