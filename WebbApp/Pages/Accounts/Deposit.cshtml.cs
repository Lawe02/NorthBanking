using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace WebbApp.Pages.Accounts
{
    [Authorize(Roles = "Cashier")] 

    public class DepositModel : PageModel
    {
        private readonly IAccountService _service;
        public DepositModel(IAccountService service)
        {
            _service = service;
        }
        [BindProperty]
        [Required]
        public int Id { get; set; }

        [BindProperty]
        [Required]
        [Range(100, 50000)]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public void OnGet(int id)
        {
            var acc = _service.GetAccount(id);

            Id = acc.AccountId;
            Balance = acc.Balance;
        }

        public IActionResult OnPost(int id, decimal amount)
        {
            if(ModelState.IsValid)
            {
                var acc = _service.GetAccount(id);
                _service.MakeDeposit(amount, acc);
                return RedirectToPage("../Customers/OneCustomer", new { id });
            }
            else 
            {
                return Page();
            }  
        }
    }
}
