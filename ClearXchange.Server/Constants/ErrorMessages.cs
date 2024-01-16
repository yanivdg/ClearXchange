namespace ClearXchange.Server.Constants
{
    public static class ErrorMessages
    {
        public static string InternalError = "Internal Server Error";
        public static string NotFound = "Member not found.";
        public static string AddingToDbErr = $"Error adding member";
        public static string MisMatchIDErr = "ID in the URL does not match the ID in the object.";
        public static string ID9DigitsErr = "ID Number must be 9 digits.";
        public static string DOBValidationErr = "Valid Date of birth is required.";
        public static string IDValidationErr = "ID number is required.";
        public static string IDNumValidationErr = "ID number must be numeric.";
        public static string NameValidationErr = "Name is required.";
        public static string EmailValidationErr = "Email is not valid.";
        public static string PhoneValidationErr = "Phone number must be numeric.";

    }
}
