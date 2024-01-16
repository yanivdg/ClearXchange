using ClearXchange.Server.Constants;
using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
                throw new ArgumentException(ErrorMessages.IDValidationErr);
            }

            if (!int.TryParse(Id, out _))
            {
                throw new ArgumentException(ErrorMessages.IDNumValidationErr);
            }
        }


        public void ValidateName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException(ErrorMessages.NameValidationErr);
            }
        }
        
        public void ValidateDateOfBirth(DateTime DateOfBirth)//format yyyy-MM-dd
        {
            // Get the current date
   
                DateTime currentDate = DateTime.Now.Date;
            if (DateOfBirth.Date == DateTime.MinValue.Date || !(DateOfBirth.Date < currentDate.Date))
            {
                throw new ArgumentException(ErrorMessages.DOBValidationErr);
            }

        }

        public void ValidateEmail(string Email) 
        {
            if (!string.IsNullOrEmpty(Email) && !IsValidEmail(Email))
            {
                throw new ArgumentException(ErrorMessages.EmailValidationErr);
            }

        }

        public void ValidatePhone(string Phone)
        { 
            if (string.IsNullOrEmpty(Phone) || !IsNumeric(Phone))
            {
                throw new ArgumentException(ErrorMessages.PhoneValidationErr);
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
            if (member != null)
            {
                ValidateID(member.Id);
                ValidateName(member.Name);
                ValidateDateOfBirth(member.DateOfBirth);
                ValidateEmail(member.Email);
                ValidatePhone(member.Phone);
            }

        }
    }

}
