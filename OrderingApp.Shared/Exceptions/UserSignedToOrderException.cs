namespace OrderingApp.Shared.Exceptions
{
    public class UserSignedToOrderException : Exception
    {
        public UserSignedToOrderException() { }
        public UserSignedToOrderException(string message) : base(message) { }

    }
}
