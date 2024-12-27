namespace Domain.Exceptions.SpaceExceptions
{
    public class SpaceAlreadyExistsException : Exception
    {
        public SpaceAlreadyExistsException(string name)
            : base($"A space with the name '{name}' already exists.")
        {
        }
    }
}
