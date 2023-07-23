using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;
using WebbApp.Models;

namespace WebbApp.Areas.Identity.Pages.Account.Manage
{
    public class AddCustomerModel : PageModel
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;
        public AddCustomerModel(ICustomerService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        [BindProperty]
        public CustomerViewModel CustomerVm { get; set; }
        public Customer Customer { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Customer = _mapper.Map(CustomerVm,Customer);
            _service.CreateCustomer(Customer);
            return Redirect("./Customermanagement");
        }
    }
}
