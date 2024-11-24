using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class UserRepository 
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new user to the database
        public void AddUser(User user)
        {
            _context.Users.Add(user);
           _context.SaveChanges();
        }

        // Get a user by name
        public User GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.UName == name);
        }

        // Delete a user by ID
        public void DeleteById(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
               _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        // Update an existing user's information
        public void UpdateUser(User updatedUser)
        {
            var existingUser = _context.Users.Find(updatedUser.UserID);
            if (existingUser != null)
            {
                existingUser.UName = updatedUser.UName;
                existingUser.Gender = updatedUser.Gender;
                existingUser.Passcode = updatedUser.Passcode;

                // Update other fields if needed
               _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        // Insert a new user (Alias for AddUser for compatibility)
        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        // Save changes to the database
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
