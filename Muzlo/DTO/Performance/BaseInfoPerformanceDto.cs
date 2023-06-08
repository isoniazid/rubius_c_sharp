public class BaseInfoPerformanceDto : IMapFrom<Performance>
{   //NB ааа сложна
    public long Id { get; set; }
    public ConcertInPerformanceDto? Concert {get; set;}
    public BaseInfoBandDto? Band { get; set; }
}