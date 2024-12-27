namespace Domain.Exceptions.SpaceExceptions
{
    public class SpaceNotFoundException : Exception
    {
        public SpaceNotFoundException(Guid spaceId)
            : base($"The space with ID '{spaceId}' was not found.")
        {
        }
    }
}
