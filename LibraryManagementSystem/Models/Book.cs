using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; } // Primary Key with auto-increment

        [Required]
        [StringLength(255)] // Limiting the length of the book name
        public string BName { get; set; } // Name of the book

        [Required]
        [StringLength(255)] // Limiting the length of the author's name
        public string Author { get; set; } // Author of the book

        [Required]
        [Range(1, int.MaxValue)] // Ensuring at least 1 copy exists
        public int TotalCopies { get; set; } // Total copies of the book available

        [Required]
        [Range(0, int.MaxValue)] // Preventing negative borrowed copies
        public int Borrowed { get; set; } // Number of copies currently borrowed

        [Required]
        [Column(TypeName = "decimal(10, 2)")] // Defining precision for price
        public decimal CopyPrice { get; set; } // Price per copy

        [Required]
        [Range(1, 365)] // Borrowing period range (1 to 365 days)
        public int AllowedBorrowingPeriod { get; set; } // Allowed borrowing period in days
                                                        // Foreign Key for Category
        [ForeignKey("Category")]
        public int? CategoryID { get; set; } // Foreign Key to Category table
        // Rating Property
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public decimal Rating { get; set; } // Rating of the book (1-5)
        // Navigation property to Category table
        public Category Category { get; set; }
        public virtual ICollection<Borrowing> Borrowings { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
