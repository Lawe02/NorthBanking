using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using WebbApp.Data;

namespace WebbApp.Areas.Identity.Pages.Account.Manage
{
    public class CrudMainModel : PageModel
    {
        private readonly IUserService _service;
        public CrudMainModel(IUserService service)
        {
            _service = service;
        }
        public List<IdentityUser> Users { get; set; }
       
        public void OnGet()
        {
            Users = _service.GetUsers();
        }
        public IActionResult OnPostDelete(string id)
        {
            _service.DeleteUser(id);
            OnGet();
            return Page();
        }
    }
}
