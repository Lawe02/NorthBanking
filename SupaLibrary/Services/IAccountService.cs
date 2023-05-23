using SupaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbApp.Models;

namespace SupaLibrary.Services
{
    public interface IAccountService
    {
        List<AccountViewModel> GetAccounts(string sortColumn, string sortOrder, int pageNr, string q);
        public Account GetAccount(int id);
        public void MakeDeposit(decimal sum, Account acc);
        public void MakeWithdrawal(decimal sum, Account acc);
        public void MakeTransfer(decimal sum, Account from, Account to);
    }
}
