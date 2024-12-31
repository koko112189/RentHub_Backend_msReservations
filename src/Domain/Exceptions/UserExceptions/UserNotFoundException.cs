namespace Domain.Exceptions.UserExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId)
            : base($"The user with ID '{userId}' was not found.")
        {
        }
    }
}
