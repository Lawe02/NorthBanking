using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupaLibrary.Services;

namespace WebbApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICountryService _service;

        public IndexModel(ILogger<IndexModel> logger, ICountryService service)
        {
            _logger = logger;
            _service = service;
        }
        public int[] TotalAccounts { get; set; } 
        public int[] TotalClients { get; set; } 
        public int[] TotalAssets { get; set; }

        public string[] Countries = new string[] { "SE", "NO","DK","FI" };
        
        public void OnGet()
        {
            TotalAccounts = _service.TotalAccounts(Countries);
            TotalClients = _service.TotalClients(Countries);
            TotalAssets = _service.TotalAssets(Countries);
        }
    }
}