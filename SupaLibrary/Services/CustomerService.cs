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
        private readonly IAccountService _accountService;
        public CustomerService(BankAppDataContext context, IAccountService accService)
        {
            _dbContext = context;
            _accountService = accService;
        }
        public List<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, string q, int pageNr)
        {
            var query = _dbContext.Customers
            .Select(x => new CustomerViewModel()
            {
                Streetaddress = x.Streetaddress,
                CustomerId = x.CustomerId,
                Givenname = x.Givenname,
                Country = x.Country,
                Gender = x.Gender,
                City = x.City,
                CountryCode = x.CountryCode,
                NationalId = x.NationalId,
                Surname = x.Surname,
                Telephonenumber = x.Telephonenumber,
                ZipCode = x.Zipcode
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

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Country);
                else
                    query = query.OrderByDescending(x => x.Country);

            if (q != null)
                query = query.Where(x => x.Streetaddress.Contains(q)
                            || x.CustomerId.ToString().Contains(q)
                            || x.Givenname.Contains(q)
                            || x.Country.Contains(q));

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
            var accounts = _dbContext.Dispositions.Include(x => x.Customer)
                                                     .Include(x => x.Account)
                                                     .Where(x => x.CustomerId == id)
                                                     .Select(x => new Account
                                                     {
                                                         AccountId = x.Account.AccountId,
                                                         Frequency = x.Account.Frequency,
                                                         Balance = x.Account.Balance,
                                                         Created = x.Account.Created,
                                                         Dispositions = x.Account.Dispositions, Loans = x.Account.Loans
                                                     })
                                                     .ToList();

            return accounts;
        }

        public decimal TotalBalance(int id)
        {
            decimal totalBalance = GetCustomerAccounts(id)
                                  .Sum(x => x.Balance);

            return totalBalance;
        }
        public void CreateCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            _accountService.CreateAccount(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            var customerToUpdate = GetCustomer(customer.CustomerId);
            customerToUpdate.Zipcode = customer.Zipcode;
            customerToUpdate.Givenname = customer.Givenname;
            customerToUpdate.Emailaddress = customer.Emailaddress;
            customerToUpdate.Streetaddress = customer.Streetaddress;
            customerToUpdate.Telephonenumber = customer.Telephonenumber;
            customerToUpdate.Gender = customer.Gender;
            customerToUpdate.CountryCode = customer.CountryCode;
            customerToUpdate.NationalId = customer.NationalId;
            customerToUpdate.City = customer.City;
            _dbContext.SaveChanges();
        }
    }
}
