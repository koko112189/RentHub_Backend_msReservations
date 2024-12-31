namespace Domain.Exceptions.UserExceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string id)
            : base($"A user with the id '{id}' already exists.")
        {
        }
    }
}
