using SupaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebbApp.Models;

namespace SupaLibrary.Services
{
    public interface ICustomerService
    {
        public List<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, string q, int pageNr);
        public List<Account> GetCustomerAccounts(int id);
        public Customer GetCustomer(int id);
        public decimal TotalBalance(int id);
    }
}
