using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupaLibrary.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string? NationalId { get; set; }
        public string Givenname { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Telephonenumber { get; set; }
        public string Country { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
    }
}