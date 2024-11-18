using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public int CountByGender(Gender gender)
        {
            return _context.Users.Count(u => u.Gender == gender);
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);  // Add user directly to the DbContext
            _context.SaveChanges();    // Save changes to the database
        }
    }
}
