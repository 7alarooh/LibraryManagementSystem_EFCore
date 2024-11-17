using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID { get; set; } // Primary Key with auto-increment

        [Required]
        [StringLength(255, ErrorMessage = "Admin name cannot exceed 255 characters.")]
        public string AName { get; set; } // Admin's name

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } // Admin's email

        [Required]
        [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
        public string Password { get; set; } // Admin's password
    }
}
