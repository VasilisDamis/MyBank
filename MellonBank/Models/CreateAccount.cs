using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models

{

  
    public class CreateAccount
    {
        [Required]
        [Display(Name = "AFM")]
        public string AFM {  get; set; } = string.Empty;

        [Required]
        [Display(Name = "AccountNumber")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "The FieldName field is required.")]
        [Display(Name = "Balance")]
        public Decimal Balance { get; set; }


        [Required(ErrorMessage = "The FieldName field is required.")]
        [Display(Name = "ManagementBranch")]

        public string ManagementBranch { get; set; } = string.Empty;


        [Required(ErrorMessage = "The FieldName field is required.")]
        [Display(Name = "AccountType")]

        public string AccountType { get; set; } = string.Empty;
    }
}
