using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace LibraryManagementSystem.Models
{
    [PrimaryKey(nameof(PurchaseID), nameof(BookID))]
    public class PurchaseDetails
    {
        [Required]
        [ForeignKey("Purchase")]
        public int PurchaseID { get; set; } // Foreign Key referencing Purchase

        [Required]
        [ForeignKey("Book")]
        public int BookID { get; set; } // Foreign Key referencing Book

        [Required]
        public int Quantity { get; set; } // Quantity of the book in the purchase

        // Navigation properties
        public Purchase Purchase { get; set; }
        public Book Book { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}

