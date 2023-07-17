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
        public int AccountId { get; set; }
        [BindProperty]
        public int CustomerId { get; set; }

        [BindProperty]
        [Required]
        [Range(100, 50000)]
        public decimal Amount { get; set; }
        [BindProperty]
        public decimal Balance { get; set; }
        public void OnGet(int id, int cusId)
        {
            var acc = _service.GetAccount(id);

            CustomerId = cusId;
            AccountId = id;
            Balance = acc.Balance;
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var acc = _service.GetAccount(AccountId);
                _service.MakeDeposit(Amount, acc);
                return RedirectToPage("../Customers/OneCustomer", new { id = CustomerId });
            }
            else 
            {
                return Page();
            }  
        }
    }
}

