using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;

namespace WebbApp.Areas.Identity.Pages.Account.Manage
{
    public class UpdateUserModel : PageModel
    {
        private readonly IUserService _service;
        public UpdateUserModel(IUserService service)
        {
            _service = service;
        }
        public IdentityUser User { get; set; }
        public void OnGet(string id)
        {
            User = _service.GetUser(id); 
        }

        public IActionResult Onpost(IdentityUser user)
        {
            _service.UpdateUser(user);
            return RedirectToPage("./CrudMain");
        }
    }
}
