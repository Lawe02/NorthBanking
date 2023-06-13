using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbApp.Models;
using SupaLibrary.ViewModels;
using System.Diagnostics.Metrics;

namespace SupaLibrary.Services
{
    public class CountryService : ICountryService
    {
        private readonly BankAppDataContext _context;
        public CountryService(BankAppDataContext context)
        {
            _context = context;
        }

        public int[] TotalAssets(string[] countries)
        {
            int[] assets = new int[countries.Length];
                
            for(int i =0;i < countries.Length; i++)
            {
                var total = _context.Dispositions.Include(x => x.Customer)
                                                 .Include(x => x.Account)
                                                 .Where(x => x.Customer.CountryCode == countries[i])
                                                 .Sum(x => x.Account.Balance);
                assets[i] = (int)total;
            }

            return assets;
        }

        public int[] TotalAccounts(string[] countries)
        {
            int[] accounts = new int[countries.Length]; 
            for(int i =0; i < countries.Length; i++)
            {
                var total = _context.Dispositions.Include(x => x.Account)
                                                 .Include(x => x.Customer)
                                                 .Where(x => x.Customer.CountryCode == countries[i])
                                                 .Count(x => x.Account.AccountId == x.AccountId);
                accounts[i] = total;
            } 
            return accounts;
        }

        public int[] TotalClients(string[] countries)
        {
            int[] clients = new int[countries.Length];
            for(int i = 0; i < countries.Length; i++)
            {
                var total = _context.Dispositions.Include(x => x.Account)
                                            .Include(x => x.Customer)
                                            .Count(x => x.Customer.CountryCode == countries[i]);
                clients[i] = total;
            }
            return clients;
        }

        public List<TopTenViewModel> TopTenAccounts(string country)
        {
            var topTen = _context.Dispositions.Include(x => x.Account)
                                              .Include(x => x.Customer)
                                              .Where(x => x.Customer.CountryCode == country)
                                              .OrderByDescending(x => x.Account.Balance)
                                              .Take(10)
                                              .Select(x => new TopTenViewModel() { Id = x.Account.AccountId, Name = x.Customer.Givenname, Email = x.Customer.Emailaddress, Balance = x.Account.Balance })
                                              .ToList();
            return topTen;
        }
    }
}
