using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;
using WebbApp.Models;

namespace WebbApp.Areas.Identity.Pages.Account.Manage
{
    public class UpdateCustomerModel : PageModel
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;
        public UpdateCustomerModel(ICustomerService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        [BindProperty]
        public CustomerViewModel CustomerVm { get; set; }
        public Customer Customer { get; set; }
        public void OnGet(int id)
        {
            Customer = _service.GetCustomer(id);
            CustomerVm = _mapper.Map(Customer, CustomerVm);
        }
        public IActionResult OnPost()
        {
            Customer = _mapper.Map(CustomerVm, Customer);
            _service.UpdateCustomer(Customer); 
            return Redirect("./Customermanagement");
        }
    }
}
