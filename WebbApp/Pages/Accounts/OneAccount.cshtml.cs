using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;
using WebbApp.Models;

namespace WebbApp.Pages.Accounts
{
    [Authorize(Roles ="Cashier")]
    public class OneAccountModel : PageModel
    {
        private readonly IAccountService _service;

        public OneAccountModel(IAccountService service)
        {
            _service = service;
        }
        public Account Account { get; set; }
        [BindProperty]
        public int PageNr { get; set; } = 1;
        public List<Transaction> Transactions { get; set; }
        public void OnGet(int id)
        {
            Account = _service.GetAccount(id);
            Transactions = _service.GetTransactions(id, PageNr);
        }

        public IActionResult OnGetShowMore(int id, int pageNr)
        {
             return new JsonResult(new { Transactions = _service.GetTransactions(id, pageNr) });
        }

    }     
}
