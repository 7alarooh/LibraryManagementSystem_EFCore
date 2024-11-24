using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)  { _context = context; }

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

        public void AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void DeleteAdmin(Admin adminID)
        {
            var admin = _context.Admins.Find(adminID);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        // Update
        public void UpdateAdmin (Admin updatedAdmin)
        {
            var existingAdmin = _context.Admins.Find(updatedAdmin.AdminID);
            if (existingAdmin != null)
            {
                existingAdmin.AName = updatedAdmin.AName;
                existingAdmin.Email = updatedAdmin.Email;
                existingAdmin.Password = updatedAdmin.Password;

                // Update other fields if needed
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public void InsertAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public List<Admin> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }

        // Method to count the total number of admins
        public int GetTotalAdmins()
        {
            return _context.Admins.Count();
        }
    }
}
