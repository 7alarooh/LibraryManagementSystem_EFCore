using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; } // Primary Key with auto-increment

        [Required]
        [StringLength(255, ErrorMessage = "User name cannot exceed 255 characters.")]
        public string UName { get; set; } // User's name

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender.")]
        public Gender Gender { get; set; } // Gender of the user (e.g., Male, Female)
        public virtual ICollection<Borrowing> Borrowings { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Passcode cannot exceed 255 characters.")]
        public string Passcode { get; set; } // User's passcode for login

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}

