using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Borrowing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BorrowingID { get; set; } // Primary Key with auto-increment

        [Required]
        public DateTime BorrowingDate { get; set; } // Date when the book is borrowed

        public DateTime? ActualReturnDate { get; set; } // Date when the book is actually returned (nullable)

        public int? Rating { get; set; } // Rating given by the user for the borrowed book

        public bool IsReturned { get; set; } // Flag indicating whether the book has been returned

        // Foreign Key references
        [ForeignKey("User")]
        public int UserID { get; set; } // Foreign Key to User table

        [ForeignKey("Book")]
        public int BookID { get; set; } // Foreign Key to Book table

        // Navigation properties
        public User User { get; set; } // Navigation property to User
        public Book Book { get; set; } // Navigation property to Book

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Book> Books { get; set; }


        // Derived Attribute - Predicted Return Date
        [NotMapped] // This is not stored in the database, it's just for calculation
        public DateTime PredictedReturnDate
        {
            get
            {
                // Calculate Predicted Return Date using AllowedBorrowingPeriod and BorrowingDate
                if (Book != null && BorrowingDate != null)
                {
                    return BorrowingDate.AddDays(Book.AllowedBorrowingPeriod);
                }
                return DateTime.MinValue; // Return a default value if conditions are not met
            }
        }
    }
}
