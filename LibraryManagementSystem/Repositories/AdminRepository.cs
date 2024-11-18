using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class AdminRepository : Repository<Admin>
    {
        public AdminRepository(ApplicationDbContext context) : base(context) { }

        // You can add specific methods related to Admin here if needed, for example:

        // Method to get an Admin by their email
        public Admin GetAdminByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a => a.Email == email);
        }

        // Method to update an Admin's password by their email
        public void UpdateAdminPassword(string email, string newPassword)
        {
            var admin = GetAdminByEmail(email);
            if (admin != null)
            {
                admin.Password = newPassword;
                _context.SaveChanges();
            }
        }

        // Method to count the total number of admins
        public int GetTotalAdmins()
        {
            return _context.Admins.Count();
        }
    }
}
