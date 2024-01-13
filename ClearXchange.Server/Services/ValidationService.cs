using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace ClearXchange.Server.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidateID(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentException("ID number is required.");
            }

            if (!int.TryParse(Id, out _))
            {
                throw new ArgumentException("ID number must be numeric.");
            }
        }

        public void ValidateName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException("Name is required.");
            }
        }
        public void ValidateDateOfBirth(DateTime DateOfBirth)
        {
            if (DateOfBirth == DateTime.MinValue)
            {
                throw new ArgumentException("Date of birth is required.");
            }

        }

        public void ValidateEmail(string Email) 
        {
            if (!string.IsNullOrEmpty(Email) && !IsValidEmail(Email))
            {
                throw new ArgumentException("Email is not valid.");
            }

        }

        public void ValidatePhone(string Phone)
        { 
            if (!string.IsNullOrEmpty(Phone) && IsNumeric(Phone))
            {
                throw new ArgumentException("Phone number must be numeric.");
            }
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
        public bool IsValidEmail(string email) 
        {
            return IsValidEmailAddress(email);
        }
        private bool IsValidEmailAddress(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public void ValidateMember(Member member)
        {
            ValidateID(member.Id);
            ValidateName(member.Name);
            ValidateDateOfBirth(member.DateOfBirth);
            ValidateEmail(member.Email);
            ValidatePhone(member.Phone);
        }
    }

}
