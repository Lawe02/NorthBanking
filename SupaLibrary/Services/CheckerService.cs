using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SupaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebbApp.Models;

namespace SupaLibrary.Services
{
    public class CheckerService : ICheckerService
    {
        private readonly BankAppDataContext _context;
        public CheckerService(BankAppDataContext context)
        {
            _context = context;
        }

        public string[] Countries { get; set; } = { "sv", "dk", "no", "fi" };
        public List<CheckVM> CheckCountry(string country)
        {
            List<CheckVM> checkList = new();
            CheckVM vm = new();
            List<TransactionVM> tVm = new();
            string query = @$"
                   DECLARE @CountryCode VARCHAR(2) = '{country}';

                   SELECT c.Givenname AS Name, a.AccountId, t.Amount, t.Date,
                       CASE
                           WHEN t.Amount >= 15000 THEN 'Transaction over 15,000'
                           WHEN t.Date >= DATEADD(DAY, -3, GETDATE()) THEN 'Deposits under 72h period exceed'
                           ELSE ''
                       END AS Reason
                   FROM Dispositions d
                   INNER JOIN Customers c ON d.CustomerId = c.CustomerId
                   INNER JOIN Accounts a ON d.AccountId = a.AccountId
                   INNER JOIN Transactions t ON a.AccountId = t.AccountId
                   WHERE c.CountryCode = @CountryCode
                       AND (
                           (t.Amount >= 15000)
                           OR (
                               t.Date >= DATEADD(DAY, -3, GETDATE())
                               AND t.Amount > 0
                               AND a.AccountId IN (
                                   SELECT AccountId
                                   FROM Transactions
                                   WHERE Date >= DATEADD(DAY, -3, GETDATE())
                                   GROUP BY AccountId
                                   HAVING SUM(Amount) > 23000
                               )
                           )
                       )
                        ORDER BY a.AccountId ASC;";
            string connectionString = "Server=localhost;Database=BankAppData;Trusted_Connection=True;MultipleActiveResultSets=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    int currentId= 0;

                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        int accountId = Convert.ToInt32(reader["AccountId"]);
                        decimal amount = Convert.ToDecimal(reader["Amount"]);
                        DateTime date = Convert.ToDateTime(reader["Date"]);
                         string reason = reader["Reason"].ToString();
                        

                        if (accountId != currentId)
                        {
                            vm.TransactionList = tVm;
                            checkList.Add(vm);
                            tVm = new();
                            vm = new();
                            vm.Name = name;
                            vm.AccountId = accountId;
                            //Console.WriteLine($"Name: {name} Account ID: {accountId}");
                            //Console.WriteLine();
                            //Console.WriteLine();
                            //Console.WriteLine();
                            currentId = accountId;
                        }
                        tVm.Add(new TransactionVM() { Amount = amount, Date = date, Reason = reason });  
                        //Console.WriteLine($"Amount: {amount} Date: {date} Reason: {reason}");
                        //Console.WriteLine();

                    }

                    reader.Close();
                }

                connection.Close();
            }
            return checkList;


            //    var check = new List<CheckVM>();
            //    var fraudulentTransactions = _context.Dispositions
            //        .Include(x => x.Customer)
            //        .Include(x => x.Account)
            //        .Include(x => x.Account.Transactions)
            //        .Where(x => x.Customer.CountryCode.ToLower() == country)
            //        .Select(x => new
            //        {
            //            Name = x.Customer.Givenname,
            //            AccountID = x.AccountId,
            //            Transactions = x.Account.Transactions.OrderBy(t => t.Date).ToList()
            //        })
            //        .ToList()
            //        .Select(x => new CheckVM
            //        {
            //            Name = x.Name,
            //            AccountId = x.AccountID,
            //            TransactionList = x.Transactions
            //                .Where(t => t.Amount >= 15000 ||
            //                            (t.Date.AddDays(3) >= DateTime.Now &&
            //                             x.Transactions.Where(tx => tx.Date.AddDays(3) >= DateTime.Now)
            //                                           .Sum(tx => tx.Amount) > 23000))
            //                .Select(t => new TransactionVM
            //                {
            //                    Amount = t.Amount,
            //                    Date = t.Date,
            //                    Id = t.TransactionId,
            //                    Reason = (t.Amount >= 15000) ? "Transaction over 15,000" : "Deposits under 72h period exceed"
            //                })
            //                .ToList()
            //        })
            //        .Where(x => x.TransactionList.Any())
            //        .ToList();

            //    foreach (var transaction in fraudulentTransactions)
            //    {
            //        Console.WriteLine(transaction.Name);
            //        Console.WriteLine(transaction.AccountId);
            //        foreach (var t in transaction.TransactionList)
            //        {
            //            Console.WriteLine($"Amount: {t.Amount} Reason: {t.Reason} Date: {t.Date}");
            //        }
            //    }
            //}


            // Switch to client-side evaluation
            //.Select(x => new CheckVM()
            //{
            //    AccountId = x.AccountID,
            //    Name = x.Name,
            //    TransactionList = x.Transactions
            //        .Where(tx => FindTimeExceed(x.AccountID, tx.TransactionId) || tx.Amount > 15000)
            //        .Select(tx => new TransactionVM
            //        {
            //            Id = tx.TransactionId,
            //            Amount = tx.Amount,
            //            Date = tx.Date,
            //            Reason = (tx.Amount > 15000) ? "Over 15,000" : "Deposits under 72h period exceed"
            //        })
            //        .ToList()
            //})
            //.ToList();


            //foreach (var transaction in fraudulentTransactions)
            //{
            //    Console.WriteLine(transaction.Name);
            //    foreach (var t in transaction.TransactionList)
            //        Console.WriteLine($"Amount: {t.Amount} Reason: {t.Reason} Date: {t.Date}");
        }
        public void WriteReport()
        {
            var norway = CheckCountry("no");
            string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "anka.txt")))
            {
                foreach (var line in norway)
                {
                    outputFile.WriteLine("AccountID : "+line.AccountId);

                    foreach(var tran in line.TransactionList)
                        outputFile.WriteLine("Amount : " + tran.Amount+"Reason :" + tran.Reason);
                }
                Console.WriteLine(docPath);    

            }
        }
    }
}
