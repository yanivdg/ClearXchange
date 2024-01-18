namespace ClearXchange.Server.Services
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string> Failures { get; }

        public ValidationException(Dictionary<string, string> failures)
        {
            Failures = failures ?? new Dictionary<string, string>();
        }
    }
}
