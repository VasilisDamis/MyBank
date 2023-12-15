using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models
{
    public class EditUser
    {
        public string Id { get; set; }
        
        [EmailAddress]
        [Display(Name = "Email")]

        public string Email { get; set; } = string.Empty;

        
        [Display(Name = "FirstName")]

        public string FirstName { get; set; } = string.Empty;

        
        [Display(Name = "LastName")]

        public string LastName { get; set; } = string.Empty;

        
        [Display(Name = "Address")]

        public string Address { get; set; } = string.Empty;

        
        [Display(Name = "AFM")]

        public string AFM { get; set; } = string.Empty;

        
        
        [Display(Name = "Phone")]

        public string Phone { get; set; } = string.Empty;

    }

}
