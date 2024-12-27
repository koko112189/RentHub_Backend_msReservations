public class Space
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}
