using Microsoft.EntityFrameworkCore;
using SupaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WebbApp.Models;

namespace SupaLibrary.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;
        public CustomerService(BankAppDataContext context)
        {
            _dbContext = context;
        }
        public List<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, string q, int pageNr)
        {
            var query = _dbContext.Customers
            .Select(x => new CustomerViewModel()
            { 
                Streetaddress = x.Streetaddress,
                CustomerId = x.CustomerId,
                Givenname = x.Givenname
            });

            if (sortColumn == "Address")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Streetaddress);
                else
                    query = query.OrderByDescending(x => x.Streetaddress);

            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.CustomerId);
                else
                    query = query.OrderByDescending(x => x.CustomerId);

            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Givenname);
                else
                    query = query.OrderByDescending(x => x.Givenname);

            if (q != null)
                query = query.Where(x => x.Streetaddress.Contains(q)
                            || x.CustomerId.ToString().Contains(q)
                            || x.Givenname.Contains(q));

            query = query.Skip((pageNr - 1) * 50)
                .Take(50);

            return query.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _dbContext.Customers.First(x => x.CustomerId == id);
        }
        public List<Account> GetCustomerAccounts(int id)
        {
            var accounts = _dbContext.Accounts.Include(x => x.Dispositions).Where(x => x.AccountId == id).ToList();
            return accounts;
        }

        public decimal TotalBalance(int id)
        {
            decimal totalBalance = GetCustomerAccounts(id)
                                  .Sum(x => x.Balance);

            return totalBalance;
        }
    }
}
