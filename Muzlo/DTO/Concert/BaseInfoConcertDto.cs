public class BaseInfoConcertDto : IMapFrom<Concert>
{

    public long Id {get; set;}
    public DateTime Date {get; set;}
    public BaseInfoHallDto Hall {get; set;} = new BaseInfoHallDto();
    public string Name {get; set;} = string.Empty;

    public List<PerformanceInConcertDto>? Performances {get; set;}

    public ConcertState State {get; set;}
}