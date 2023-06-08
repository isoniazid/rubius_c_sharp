public class Performance
{
    public long Id { get; set; }
    public long ConcertId { get; set; }

    public Concert Concert { get; set; } = new();
    public long BandId { get; set; }
    public Band Band { get; set; } = new();
}