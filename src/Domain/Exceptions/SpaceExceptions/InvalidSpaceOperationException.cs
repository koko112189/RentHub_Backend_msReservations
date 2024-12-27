namespace Domain.Exceptions.SpaceExceptions
{
    public class InvalidSpaceOperationException : Exception
    {
        public InvalidSpaceOperationException(string message)
            : base(message)
        {
        }
    }
}
