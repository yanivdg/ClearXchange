using ClearXchange.Server.Constants;
using System.ComponentModel.DataAnnotations;

namespace ClearXchange.Server.Model
{
    public class Member
    {
        [Key]
        [Required]
        [StringLength(9, ErrorMessage = "ID Number must be 9 digits.")]
        [RegularExpression(@"^0*[1-9]\d*$", ErrorMessage = "ID must contain digits and can have leading zeros.")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        //[RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Invalid Gender")]
        public Gender? Gender { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Telephone must be numeric.")]
        public string? Phone { get; set; }

    }

}
