using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SupaLibrary.ViewModels
{
    public class CheckVM
    {
        public int AccountId { get; set; } 
        public string Name { get; set; }
        public List<TransactionVM> TransactionList { get; set; }
    }
}
