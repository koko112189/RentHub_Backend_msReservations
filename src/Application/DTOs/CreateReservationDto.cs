using System.ComponentModel.DataAnnotations;

public class CreateReservationDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid SpaceId { get; set; }
    [Required]
    public DateTime StartDateTime { get; set; }
    [Required]
    public DateTime EndDateTime { get; set; }
}
