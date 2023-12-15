using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models
{
    public class ChangePasswordModel
    {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current Password")]
            public string CurrentPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm New Password")]
            [Compare("NewPassword", ErrorMessage = "Τα στοιχεια που δωσατε δν ειναι ιδια")]
            public string ConfirmNewPassword { get; set; }
    }


}
