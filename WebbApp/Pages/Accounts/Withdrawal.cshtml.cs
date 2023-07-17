using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebbApp.Models;

namespace WebbApp.Pages.Accounts
{
    [Authorize(Roles = "Cashier")]

    public class WithdrawalModel : PageModel
    {
        private readonly IAccountService _service;

        public WithdrawalModel(IAccountService service)
        {
            _service = service;
        }
        [BindProperty]
        public int CustomerId { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public decimal Balance { get; set; }

        [BindProperty]
        [Range(100, 50000)]
        [Required]
        public decimal Amount { get; set; }
        public void OnGet(int id, int cusId)
        {
            var acc = _service.GetAccount(id);
            CustomerId = cusId;
            Id = acc.AccountId;
            Balance = acc.Balance;
        }
           
        public IActionResult OnPost()
        {
            if(Balance < Amount)
            {
                ModelState.AddModelError(
                    "Amount", "You don not have that much money");
            }
            if(ModelState.IsValid)
            {
                var acc = _service.GetAccount(Id);
                _service.MakeWithdrawal(Amount, acc);
                return RedirectToPage("../Customers/OneCustomer", new { id = CustomerId });
            }
            else
                return Page();
        }
    }
}
