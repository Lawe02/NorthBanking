using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupaLibrary.Services
{
    public interface IUserService
    {
        public List<IdentityUser> GetUsers();
        public IdentityUser GetUser(string username);
        public void DeleteUser(string id);
        public void UpdateUser(IdentityUser user, bool role);
    }
}
