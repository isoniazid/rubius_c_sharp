public class AddBandPerformanceDto : IMapTo<Performance>
{
    public long ConcertId { get; set; }
    public long BandId { get; set; }
}