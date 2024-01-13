using ClearXchange.Server.Constants;
using System.ComponentModel.DataAnnotations;

namespace ClearXchange.Server.Model
{
    public class Member
    {
        [Key]
        [Required]
        [StringLength(9, ErrorMessage = "ID Number must be 9 digits.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "ID Number must be numeric.")]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^(MALE|FEMALE|OTHER)$", ErrorMessage = "Invalid Gender")]
        public string? Gender { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Telephone must be numeric.")]
        public string Phone { get; set; }
        
        public Member(string id,string name,string email,string phone)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException( nameof(email));
            Phone = phone ?? throw new ArgumentNullException(phone);
        }
        
       

    }

}
