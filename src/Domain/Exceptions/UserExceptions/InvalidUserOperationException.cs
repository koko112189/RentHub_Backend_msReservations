namespace Domain.Exceptions.UserExceptions
{
    public class InvalidUserOperationException : Exception
    {
        public InvalidUserOperationException(string message)
            : base(message)
        {
        }
    }
}
