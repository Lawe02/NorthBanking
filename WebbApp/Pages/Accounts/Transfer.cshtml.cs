using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;

namespace WebbApp.Pages.Accounts
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService _service;
        public TransferModel(IAccountService service)
        {
            _service = service;
        }

        public int Id { get; set; }
        public decimal Balance { get; set; }

        public void OnGet(int id)
        {
            var acc = _service.GetAccount(id);
            Balance = acc.Balance;
            Id = acc.AccountId;
        }

        public IActionResult OnPost(int idTo, int id, decimal amount)
        {
            var to = _service.GetAccount(idTo);
            var from = _service.GetAccount(id);
            _service.MakeTransfer(amount, from, to);

            return RedirectToPage("Index", new { id });
        }
    }
}
