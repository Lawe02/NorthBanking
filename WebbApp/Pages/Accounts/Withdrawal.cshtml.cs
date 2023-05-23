using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;

namespace WebbApp.Pages.Accounts
{
    public class WithdrawalModel : PageModel
    {
        private readonly IAccountService _service;

        public WithdrawalModel(IAccountService service)
        {
            _service = service;
        }
        public int Id { get; set; }
        public decimal Balance { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }
        public void OnGet(int id)
        {
            var acc = _service.GetAccount(id);

            Id = acc.AccountId;
            Balance = acc.Balance;
        }

        public IActionResult OnPost(int id)
        {
            if(Balance < Amount)
            {
                ModelState.AddModelError(
                    "Amount", "You don not have that much money");
            }
            if(ModelState.IsValid)
            {
                var acc = _service.GetAccount(id);
                _service.MakeWithdrawal(Amount, acc);
                return RedirectToPage("Index", new { id });
            }
            else
            {
                return Page();
            }
        }
    }
}
