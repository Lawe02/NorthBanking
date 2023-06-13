using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;
using SupaLibrary.ViewModels;

namespace WebbApp.Pages.Shared
{
    public class CountryModel : PageModel
    {
        private readonly ICountryService _service;
        public CountryModel(ICountryService service)
        {
            _service = service;
        }
        public List<TopTenViewModel> Customers { get; set; }
        public void OnGet(string id)
        {
            Customers = _service.TopTenAccounts(id);
        }
    }
}
