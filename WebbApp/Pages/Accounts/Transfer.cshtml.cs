using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using WebbApp.Models;

namespace WebbApp.Pages.Accounts
{
    [Authorize(Roles = "Cashier")]

    public class TransferModel : PageModel
    {
        private readonly IAccountService _service;
        public TransferModel(IAccountService service)
        {
            _service = service;
        }
        [Required]
        [BindProperty]
        public int Id { get; set; }
        [Required]
        [BindProperty]
        public decimal Balance { get; set; }
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }
        [Required]
        [BindProperty]
        [Range(100, 100000)]
        public decimal Amount { get; set; }

        public void OnGet(int id)
        {
            AccountFrom = _service.GetAccount(id);
            Balance = AccountFrom.Balance;
            Id = AccountFrom.AccountId;
        } 

        public IActionResult OnPost(int idTo, int id, decimal amount)
        {
            AccountTo = _service.GetAccount(idTo);

            if(Amount > Balance)
                ModelState.AddModelError(
                    "Amount", "You can not transfer that much money");
            
            if(AccountTo == null)
                ModelState.AddModelError(
                    "AccountTo", "That account does not exist");

            if(ModelState.IsValid)
            {
                var to = _service.GetAccount(idTo);
                var from = _service.GetAccount(id);
                _service.MakeTransfer(amount, from, to);
                return RedirectToPage("../Customers/OneCustomer", new { id });
            }
            else
                return Page();
        }
    }
}
