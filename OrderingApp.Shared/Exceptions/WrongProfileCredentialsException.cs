namespace OrderingApp.Shared.Exceptions
{
    public class WrongProfileCredentialsException : Exception
    {
        public WrongProfileCredentialsException() { }
        public WrongProfileCredentialsException(string message) : base(message) { }
    }
}
