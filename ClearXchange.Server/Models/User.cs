using ClearXchange.Server.Constants;
using System.ComponentModel.DataAnnotations;

namespace ClearXchange.Server.Model
{
    public class User
    {
        [Required]
        [StringLength(9, ErrorMessage = "ID Number must be 9 digits.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "ID Number must be numeric.")]
        public string IDNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Invalid Gender")]
        public Gender? Gender { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Telephone must be numeric.")]
        public string Telephone { get; set; }

        public User(string idNumber)
        {
            IDNumber = idNumber ?? throw new ArgumentNullException(nameof(idNumber));
            Name = Name ?? throw new ArgumentNullException(nameof(Name));
            Email = Email ?? throw new ArgumentNullException( nameof(Email));
            Telephone = Telephone ?? throw new ArgumentNullException(Telephone);

            

        }

    }

}
