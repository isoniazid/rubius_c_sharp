public class BandMember
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Band Band { get; set; } = new();

    public long BandId { get; set; }
}