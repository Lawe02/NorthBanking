using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupaLibrary.Services;

namespace TransactionChecker
{
    public class Checker
    {
        private readonly ICheckerService _checkerService;
        public Checker(ICheckerService service) 
        {
            _checkerService = service;
        }
        public void Check()
        {
            _checkerService.WriteReport();
        }
    }
}
