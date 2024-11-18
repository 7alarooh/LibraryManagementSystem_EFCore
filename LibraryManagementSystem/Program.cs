﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string AName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public int UID { get; set; }
        public string Uname { get; set; }
        public string Passcode { get; set; }
        public string Gender { get; set; }
    }
    public class Program
    {
        static int index = -1;
        public static async Task Main(string[] args)
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
                            var user = userRepository.GetAll().FirstOrDefault(u => u.UID == uIDValue); // Assuming `userRepository` is accessible

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
            Console.WriteLine($"Welcome to {adminName} Menu.");
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("- Accounts Management -");
                Console.WriteLine("\n Enter the No. of operation you need :");
                Console.WriteLine("\n 1 .Accounts Management");
                Console.WriteLine("\n 2 .Books Management");
                Console.WriteLine("\n 0 .SignOut");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        accountsManagement(adminID,adminName);
                        break;
                    case "2":
                        Console.Clear();
                        BooksManagement(adminID, adminName);
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("\nPress Enter key to exit out of the system");
                        string outsystem = Console.ReadLine();
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Sorry, your choice was wrong!!");
                        break;
                }
                Console.WriteLine("Press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);
        }
        static void userMenu(User user)
        {
            // Example function for user menu
            Console.WriteLine($"Welcome {user.Uname} to the User Menu.");
            // Add more user operations here
        }
        static void accountsManagement(int adminID, string adminName)
        {
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("- Accounts Management -");
                Console.WriteLine("\n Enter the No. of operation you need :");
                Console.WriteLine("\n 1 .Add new user");
                Console.WriteLine("\n 2 .Edit user information");
                Console.WriteLine("\n 3 .Remove user account");
                Console.WriteLine("\n 4 .Sub Admin Menu");
                Console.WriteLine("\n 0 .SignOut");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AddNewUser();
                        break;
                    case "2":
                        Console.Clear();
                        EditUserInformation();
                        break;
                    case "3":
                        Console.Clear();
                        //RemoveUserAccount();
                        break;
                    case "4":
                        Console.Clear();
                        BooksManagement(adminID, adminName);
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("\nPress Enter key to exit out of the system");
                        string outsystem = Console.ReadLine();
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Sorry, your choice was wrong!!");
                        break;
                }
                Console.WriteLine("Press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);
        }
        //                 add user
        public static void AddNewUser()
        {
            Console.WriteLine("--------Add New User---------\n");
            Console.WriteLine("Would you like to add a new User or Admin?");
            Console.WriteLine("Enter '1' for User or '2' for Admin:");
            string choice = Console.ReadLine();

            // Assuming ApplicationDbContext is available
            var context = new ApplicationDbContext();  // Replace with your actual DbContext
            var userRepository = new UserRepository(context);  // Instantiate the repository with the context
            var adminRepository = new AdminRepository(context);  // Instantiate the admin repository

            switch (choice)
            {
                case "1": // Add a new User
                    AddUser(userRepository);
                    break;

                case "2": // Add a new Admin
                          AddAdmin(adminRepository);  // Implement Admin addition logic
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter '1' for User or '2' for Admin.");
                    break;
            }
        }

        public static void AddUser(UserRepository userRepository)
        {
            // Take user inputs
            Console.WriteLine("Enter user name (Max 50 characters): ");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter gender (Male/Female): ");
            string genderInput = Console.ReadLine();
            Gender gender = Gender.Male; // Default value
            if (!Enum.TryParse(genderInput, true, out gender))
            {
                Console.WriteLine("Invalid gender. Defaulting to Male.");
            }

            Console.WriteLine("Enter passcode (Max 20 characters): ");
            string passcode = Console.ReadLine();

            // Validate user inputs using DataAnnotations
            var newUser = new User
            {
                Uname = userName,
                gender = gender,
                Passcode = passcode
            };

            var validationResults = ValidateUser(newUser);

            if (validationResults.Any())
            {
                // Show validation errors to the user
                Console.WriteLine("The following errors occurred while validating the user:");
                foreach (var error in validationResults)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return;
            }

            // If validation passes, add user to the database
            userRepository.AddUser(newUser); // Use the AddUser method we created
            Console.WriteLine("User added successfully.");
        }

        // Helper method for validating the user object using DataAnnotations
        public static IList<ValidationResult> ValidateUser(User user)
        {
            var validationContext = new ValidationContext(user, null, null);
            var validationResults = new List<ValidationResult>();

            // Validate using DataAnnotations
            Validator.TryValidateObject(user, validationContext, validationResults, true);

            return validationResults;
        }
        public static void AddAdmin(AdminRepository adminRepository)
        {
            // Take admin inputs
            Console.WriteLine("Enter admin name (Max 255 characters): ");
            string adminName = Console.ReadLine();

            Console.WriteLine("Enter admin email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter admin password (Max 255 characters): ");
            string password = Console.ReadLine();

            // Validate admin inputs using DataAnnotations
            var newAdmin = new Admin
            {
                AName = adminName,
                Email = email,
                Password = password
            };

            var validationResults = ValidateAdmin(newAdmin);

            if (validationResults.Any())
            {
                // Show validation errors to the user
                Console.WriteLine("The following errors occurred while validating the admin:");
                foreach (var error in validationResults)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return;
            }

            // If validation passes, add admin to the database
            adminRepository.Add(newAdmin);
            adminRepository.SaveChanges(); // Save changes to the database
            Console.WriteLine("Admin added successfully.");
        }

        // Helper method for validating the admin object using DataAnnotations
        public static IList<ValidationResult> ValidateAdmin(Admin admin)
        {
            var validationContext = new ValidationContext(admin, null, null);
            var validationResults = new List<ValidationResult>();

            // Validate using DataAnnotations
            Validator.TryValidateObject(admin, validationContext, validationResults, true);

            return validationResults;
        }
        //             edit user
        static void EditUserInformation()
        {

            Console.WriteLine("Edit User Information");
            ViewAllUsers(adminRepository);
            Console.WriteLine("Enter the type of account to edit (user/admin):");
            string accountType = Console.ReadLine().ToLower();

            switch (accountType)
            {
                case "user":
                 //   EditUser();
                    break;
                case "admin":
                    //EditAdmin();
                    break;
                default:
                    Console.WriteLine("Error: Invalid account type. Please enter 'user' or 'admin'.");
                    break;
            }
        }
        static void ViewAllUsers(AdminRepository adminRepository)
        {
            List<Admin> Admins = new List<Admin>();  // Declare it somewhere in your class or program.
            var admins = adminRepository.GetAll(); // Example fetching all admins from repository

            // Use StringBuilder for efficient string concatenation
            StringBuilder sb = new StringBuilder();

            // Define column widths for a clean, consistent layout
            int idWidth = 10;
            int nameWidth = 25;
            int emailWidth = 30;
            int passwordWidth = 20;

            // Clear StringBuilder to ensure no previous content is repeated
            sb.Clear();

            // Print Admins
            sb.AppendLine("\n\t--- All Users in Library System ---");
            sb.AppendLine("\n\t--- Admins ---");
            sb.AppendFormat("\t{0,-" + idWidth + "} {1,-" + nameWidth + "} {2,-" + emailWidth + "} {3,-" + passwordWidth + "}",
                            "ID", "Name", "Email", "Password");
            sb.AppendLine();
            sb.AppendLine(new string('-', idWidth + nameWidth + emailWidth + passwordWidth + 12)); // Separator line

            // Assuming 'Admins' is a list of admin objects, iterate over them
            foreach (var admin in Admins)
            {
                sb.AppendFormat("\t{0,-" + idWidth + "} {1,-" + nameWidth + "} {2,-" + emailWidth + "} {3,-" + passwordWidth + "}",
                                admin.AdminID,    // Assuming Admin has AdminID
                                admin.AName,      // Admin name
                                admin.Email,      // Admin email
                                admin.Password);  // Admin password
                sb.AppendLine();
            }

            // Print Users
            sb.AppendLine("\n\n\t--- Users ---");
            sb.AppendFormat("\t{0,-" + idWidth + "} {1,-" + nameWidth + "} {2,-" + emailWidth + "} {3,-" + passwordWidth + "}",
                            "ID", "Name", "Email", "Password");
            sb.AppendLine();
            sb.AppendLine(new string('-', idWidth + nameWidth + emailWidth + passwordWidth + 12)); // Separator line

            // Assuming 'Users' is a list of user objects, iterate over them
            foreach (var user in Users)
            {
                sb.AppendFormat("\t{0,-" + idWidth + "} {1,-" + nameWidth + "} {2,-" + emailWidth + "} {3,-" + passwordWidth + "}",
                                user.UID,         // Assuming User has UID
                                user.Uname,       // User name
                                user.Email,       // User email
                                user.Passcode);   // User passcode
                sb.AppendLine();
            }

            // Output the formatted string
            Console.WriteLine(sb.ToString());
        }


        static void BooksManagement(int adminID, string adminName)
        {
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("Welcome Admin in Library");
                Console.WriteLine("\n Enter the No. of operation you need :");
                Console.WriteLine("\n 1 .Add New Book");
                Console.WriteLine("\n 2 .Display All Books");
                Console.WriteLine("\n 3 .Search for Book by Name");
                Console.WriteLine("\n 4 .Edit a Book");
                Console.WriteLine("\n 5 .Remove a Book");
                Console.WriteLine("\n 6 .Report");
                Console.WriteLine("\n 7 .SignOut");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        //AddNewBook();
                        break;

                    case "2":
                        Console.Clear();
                        //ViewAllBooks();
                        break;
                    case "3":
                        Console.Clear();
                        //SearchForBook();
                        break;
                    case "4":
                        Console.Clear();
                        //EditBook();
                        break;
                    case "5":
                        Console.Clear();
                        //RemoveBook();
                        break;
                    case "6":
                        Console.Clear();
                        //Reporting();
                        break;
                    case "7":
                        Console.WriteLine("\nPress Enter key to exit out of the system");
                        string outsystem = Console.ReadLine();
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Sorry, your choice was wrong!!");
                        break;
                }

                Console.WriteLine("Press Enter key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);
        }
    }
   



}


