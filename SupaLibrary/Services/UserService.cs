using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebbApp.Data;

namespace SupaLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(ApplicationDbContext dbCOntext, UserManager<IdentityUser> userManager) 
        {
            _context = dbCOntext;
            _userManager = userManager;
        }    
        public List<IdentityUser> GetUsers()
        {
           var users = _context.Users
                .Select(x =>
                new IdentityUser
                {  
                    UserName = x.UserName,
                    Email = x.Email,
                    EmailConfirmed = x.EmailConfirmed,
                    Id = x.Id
                })
                .ToList();

            return users;
        }
        public IdentityUser GetUser(string id)
        {
            var user = _context.Users.First(x => x.Id == id);
            return user;
        }
        public void DeleteUser(string id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(IdentityUser user, bool role)
        {
            var userToUpdate = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
            userToUpdate.EmailConfirmed = user.EmailConfirmed;
            if (role)
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
                _userManager.RemoveFromRoleAsync(user, "Cashier").Wait();
            }
            else
            {
                _userManager.AddToRoleAsync(user, "Cashier").Wait();
                _userManager.RemoveFromRoleAsync(user, "Admin").Wait();
            }
            _context.SaveChanges();
        }
    }
}
