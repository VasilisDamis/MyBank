using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models
{
    public class CreateUser
    {

        [Required]
        [EmailAddress]
        [Display(Name="Email")]

        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "FirstName")]

        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "LastName")]

        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address")]

        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "AFM")]

        public string AFM { get; set; } = string.Empty;
       
        [Display(Name = "UserName")]

        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Password")]
        [PasswordPropertyText]

        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Phone")]

        public string Phone { get; set; } = string.Empty;


    }
}
