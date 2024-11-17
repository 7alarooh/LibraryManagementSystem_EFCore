using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; } // Primary Key with auto-increment

        [Required]
        [StringLength(255, ErrorMessage = "Category name cannot exceed 255 characters.")]
        public string CName { get; set; } // Name of the category

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of books cannot be negative.")]
        public int NumberOfBooks { get; set; } // Total number of books in the category
        // Navigation property to related books
        public ICollection<Book> Books { get; set; }
    }
}
