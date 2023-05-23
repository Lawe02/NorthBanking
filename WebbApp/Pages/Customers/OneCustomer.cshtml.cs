using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;
using WebbApp.Models;

namespace WebbApp.Pages.Customers
{
    [Authorize(Roles ="Admin")]
    public class OneCustomerModel : PageModel
    {
        private readonly ICustomerService _cusService;
        private readonly IMapper _mapper;
        public OneCustomerModel(ICustomerService service, IMapper mapper)
        {
            _cusService = service;
            _mapper = mapper;
        }

        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public int Id { get; set; }
        public CustomerViewModel CustomerVm { get; set; } 
        public Customer C { get; set; }
        public List<Account> Accounts { get; set; }
        public void OnGet(int id)
        {
            C = _cusService.GetCustomer(id);
            CustomerVm = _mapper.Map(C, CustomerVm);
            Accounts = _cusService.GetCustomerAccounts(id);
        }
    }
}
