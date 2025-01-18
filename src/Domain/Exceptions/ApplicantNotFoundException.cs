
namespace Domain.Exceptions
{
    public class ApplicantNotFoundException : Exception
    {

        public ApplicantNotFoundException(Guid userId)
            : base($"The applicant with ID '{userId}' was not found.")
        {
        }

    }
}