using System;
using System.Linq;
using System.Text.RegularExpressions;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem
{
    public class Program
    {
        static int index = -1;
        static async Task Main(string[] args)
        {
            using var dbContext = new ApplicationDbContext();
            var adminRepository = new AdminRepository(dbContext);
            var bookRepository = new BookRepository(dbContext);
            var borrowingRepository = new BorrowingRepository(dbContext);
            var categoryRepository = new CategoryRepository(dbContext);
            var purchaseRepository = new PurchaseRepository(dbContext);
            var userRepository = new UserRepository(dbContext);

            loginPage(adminRepository, userRepository);
        }
        //
        static void loginPage(AdminRepository adminRepository, UserRepository userRepository)
        {
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("------------Welcome to Library------------");
                Console.WriteLine("\n----------------Login Page----------------");
                Console.WriteLine("\nEnter the No of what you are:");
                Console.WriteLine("\n1. Admin Access");
                Console.WriteLine("\n2. User Access");
                Console.WriteLine("\n3. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": // Admin login
                        Console.Clear();
                        Console.Write("Enter Your Email: ");
                        string aEmail = Console.ReadLine();

                        // Find the admin by email
                        var admin = adminRepository.GetAdminByEmail(aEmail); // Assuming `adminRepository` is accessible

                        if (admin != null)
                        {
                            Console.Write("\nEnter Password: ");
                            string enterPW = Console.ReadLine();

                            // Validate admin password
                            if (enterPW == admin.Password)
                            {
                                Console.WriteLine("Admin login successful.");
                                adminMenu(admin.AdminID,admin.AName); // Assuming `adminMenu` is a method to show the admin's menu
                            }
                            else
                            {
                                Console.WriteLine("Invalid password. Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Admin not found.");
                        }
                        break;

                    case "2":// User login
                        Console.Clear();
                        Console.Write("Enter Your UID: ");
                        string uUID = Console.ReadLine();

                        // Validate UID format (example: only numeric)
                        if (!Regex.IsMatch(uUID, @"^[0-9]+$"))
                        {
                            Console.WriteLine("Invalid UID format.");
                            break;
                        }

                        // Convert UID to integer and find the user
                        if (int.TryParse(uUID, out int uIDValue))
                        {
                            var user = userRepository.GetAll().FirstOrDefault(u => u.UserID == uIDValue); // Assuming `userRepository` is accessible

                            if (user != null)
                            {
                                Console.Write("\nEnter Password: ");
                                string enterPW = Console.ReadLine();

                                // Validate user password
                                if (enterPW == user.Passcode)
                                {
                                    Console.WriteLine("User login successful.");
                                    userMenu(user); // Pass user object
                                }
                                else
                                {
                                    Console.WriteLine("Invalid password. Please try again.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: User not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid UID format.");
                        }
                        break;

                    case "3": // Exit the application
                        Console.WriteLine("\nPress Enter key to exit the system");
                        Console.ReadLine();
                        ExitFlag = true; // Exit the loop
                        break;

                    default:
                        Console.WriteLine("Sorry, your choice was wrong!!");
                        break;
                }

                // Continue only if the user hasn't chosen to exit
                if (!ExitFlag)
                {
                    Console.WriteLine("Press any key to continue to the Home Page");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (!ExitFlag);
        }

        static void adminMenu(int adminID, string adminName)
        {
            // Example function for admin menu
            Console.WriteLine("Welcome to Admin Menu.");
            // Add more admin operations here
        }

        static void userMenu(User user)
        {
            // Example function for user menu
            Console.WriteLine($"Welcome {user.UName} to the User Menu.");
            // Add more user operations here
        }
    }



}


