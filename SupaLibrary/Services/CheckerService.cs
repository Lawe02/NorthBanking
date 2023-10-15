using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SupaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public string[] Countries { get; set; } = { "se", "dk", "no", "fi" };
        public string FolderPath { get; set; }
        public List<CheckVM> CheckCountry(string country)
        {
            var lastCheck = LastCheck();
            string query = @$"
                   DECLARE @CountryCode VARCHAR(2) = '{country}';
                   DECLARE @LastCheckDate DATETIME = '{lastCheck}';

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
                           AND (t.Date >= @LastCheckDate)
                           OR (
                               t.Date >= DATEADD(DAY, -3, GETDATE())
                               AND t.Amount > 0
                               AND (t.Date >= @LastCheckDate)
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

            List<CheckVM> checkList = new();
            CheckVM vm = new();
            List<TransactionVM> tVm = new();

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

                            currentId = accountId;
                        }
                        tVm.Add(new TransactionVM() { Amount = amount, Date = date, Reason = reason });  

                    }

                    reader.Close();
                }

                connection.Close();
            }
            return checkList;


         
        }
        public void WriteReport()
        {

                foreach (string country in Countries)
                {
                    var content = CheckCountry(country);
                    var docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "rapport.txt");
                    using StreamWriter outputFile = new ((docPath), true);
                        // Write the string array to a new file named "checklist.txt".

                        foreach (var line in content)
                        {
                            outputFile.WriteLine("AccountID : " + line.AccountId + " Name : " + line.Name);

                            foreach (var tran in line.TransactionList)
                                outputFile.WriteLine("Amount : " + tran.Amount + " Reason : " + tran.Reason + " Date : " + tran.Date);
                        }
                    outputFile.WriteLine($"{country} checked, check done at {DateTime.Now}");
                }
                    

        }

        public DateTime LastCheck()
        {
            var docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "rapport.txt");

            DateTime lastDateTime = new DateTime(2002, 02, 02);
            DateTime tempDate = lastDateTime;

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(docPath);

                // Loop through each line and find the last date-time entry
                foreach (string line in lines)
                {
                    if (line.Contains("Date"))
                    {
                        tempDate = DateTime.Parse(line.Substring(line.Length - 19, 19));
                        if (lastDateTime <= tempDate)
                            lastDateTime = tempDate;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }

            // If the last date time was found, parse it to a DateTime object
            return lastDateTime;
            
        }
    }
}
