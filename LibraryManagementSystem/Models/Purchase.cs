using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseID { get; set; } // Primary Key

        [Required]
        public DateTime PurchaseDate { get; set; } // Date of purchase

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; } // Foreign Key referencing User

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } // Total price of the purchase

        // Navigation property
        public User User { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

