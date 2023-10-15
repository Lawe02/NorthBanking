using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SupaLibrary.ViewModels;
using System.Data;
using WebbApp.Models;


namespace SupaLibrary.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _context;

        public AccountService(BankAppDataContext context)
        {
            _context = context;
        }
        public List<AccountViewModel> GetAccounts(string sortColumn, string sortOrder, int pageNr, string q)
        {
            var query = _context.Accounts
                .Select(x => new AccountViewModel()
                {
                    Balance = x.Balance,
                    Created = x.Created,
                    Frequency = x.Frequency,
                    Id = x.AccountId
                });

            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.GetType().ToString() == sortColumn);
                else
                    query = query.OrderByDescending(x => x.Id);

            if (sortColumn == "Balance")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Balance);
                else
                    query = query.OrderByDescending(x => x.Balance);

            if (sortColumn == "Created")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Created);
                else
                    query = query.OrderByDescending(x => x.Created);

            if (sortColumn == "Frequency")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Frequency);
                else
                    query = query.OrderByDescending(x => x.Frequency);

            if (q != null)
                query = query.Where(x => x.Id.ToString().Contains(q) ||
                                         x.Created.ToString().Contains(q) ||
                                         x.Balance.ToString().Contains(q) ||
                                         x.Frequency.Contains(q));

            query = query
                .Skip((pageNr - 1) * 50)
                .Take(50);

            return query.ToList();

        }

        public Account GetAccount(int id)
        {
            var acc = _context.Accounts.FirstOrDefault(x => x.AccountId == id);
            return acc;
        }

        public void MakeWithdrawal(decimal amount, Account acc)
        {
            acc.Balance -= amount;

            _context.Transactions.Add(new Transaction()
            {
                Amount = -1 * amount,
                AccountId = acc.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Credit in Cash",
                Balance = acc.Balance
            });

            _context.SaveChanges();
        }

        public void MakeDeposit(decimal amount, Account acc)
        {
            acc.Balance += amount;

            _context.Transactions.Add(new Transaction()
            {
                Amount = amount,
                AccountId = acc.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Credit in Cash",
                Balance = acc.Balance
            });
            _context.SaveChanges();
        }

        public void MakeTransfer(decimal amount, Account from, Account to)
        {
            MakeDeposit(amount, to);
            MakeWithdrawal(amount, from);
        }

        public List<Transaction> GetTransactions(int id, int page)
        {
            var transaction = _context.Transactions.Where(x => x.AccountId == id)
                                                   .OrderByDescending(x => x.Date)     
                                                   .Skip((page-1) * 20)
                                                   .Take(20)
                                                   .ToList();
            return transaction;
        }
        public void CreateAccount(Customer customer)
        {
            Account account = new();
            account.Created = DateTime.Now;
            account.Frequency = "AfterTransaction";

            Disposition disposition = new();
            disposition.Account = account;
            disposition.AccountId = account.AccountId;
            disposition.Customer = customer;
            disposition.CustomerId = customer.CustomerId;
            disposition.Type = "OWNER";

            _context.Dispositions.Add(disposition);
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
    }
}
