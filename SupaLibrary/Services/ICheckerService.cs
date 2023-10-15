using SupaLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupaLibrary.Services
{
    public interface ICheckerService
    {
        public List<CheckVM> CheckCountry(string country);
        public void WriteReport();
        public string[] Countries { get; }
        public string FolderPath  { get; }
        public DateTime LastCheck();
    }
}
