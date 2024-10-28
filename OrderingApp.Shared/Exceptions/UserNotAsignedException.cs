namespace OrderingApp.Shared.Exceptions
{
    public class UserNotAsignedException : Exception
    {
        public UserNotAsignedException() { }
        public UserNotAsignedException(string message) : base(message) { }

    }
}
