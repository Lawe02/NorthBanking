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
        public string NationalId { get; set; } = null!;
        public string Givenname { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Emailaddress { get; set; }
        public string? Telephonenumber { get; set; }
        public string Country { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
    }
}