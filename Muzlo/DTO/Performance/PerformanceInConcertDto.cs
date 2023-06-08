public class PerformanceInConcertDto : IMapFrom<Performance>
{
    public long Id { get; set; }
    public BandInPerformanceDto? Band { get; set; }
}