using System.ComponentModel.DataAnnotations;

namespace EF_Pagination_Example.ViewModels
{
    public class LoginRequestViewModel
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [EmailAddress(ErrorMessage = "Field {0} is in invalid format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}