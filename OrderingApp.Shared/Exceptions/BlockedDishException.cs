namespace OrderingApp.Shared.Exceptions
{
    public class BlockedDishException : Exception
    {
        public BlockedDishException() { }
        public BlockedDishException(string message) : base(message) { }

    }
}
