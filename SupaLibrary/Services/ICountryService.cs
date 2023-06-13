using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupaLibrary.ViewModels;

namespace SupaLibrary.Services
{
    public interface ICountryService
    {
        public int[] TotalAssets(string[] countries);
        public int[] TotalAccounts(string[] countries);
        public int[] TotalClients(string[] countries);
        public List<TopTenViewModel> TopTenAccounts(string country);
    }
}
