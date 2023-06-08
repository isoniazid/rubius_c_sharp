public class ConcertInPerformanceDto : IMapFrom<Concert>
{
    public DateTime Date {get; set;}

    public string Name {get; set;} = string.Empty;
}
