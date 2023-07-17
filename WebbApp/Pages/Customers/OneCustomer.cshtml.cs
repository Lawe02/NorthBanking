using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;
using WebbApp.Models;

namespace WebbApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]

    public class OneCustomerModel : PageModel
    {
        private readonly ICustomerService _cusService;
        private readonly IMapper _mapper;
        public OneCustomerModel(ICustomerService service, IMapper mapper)
        {
            _cusService = service;
            _mapper = mapper;
        }
        public CustomerViewModel CustomerVm { get; set; } 
        public Customer C { get; set; }
        public List<Account> Accounts { get; set; }
        public decimal TotalBalance { get; set; }
        public void OnGet(int id)
        {
            C = _cusService.GetCustomer(id);
            CustomerVm = _mapper.Map(C, CustomerVm);
            Accounts = _cusService.GetCustomerAccounts(id);
            TotalBalance = _cusService.TotalBalance(id);
        }
    }
}
