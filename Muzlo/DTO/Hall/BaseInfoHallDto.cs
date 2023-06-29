public class BaseInfoHallDto : IMapFrom<Hall>
{
    public long Id {get; set;}
    public long MaxViewers {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Address {get; set;} = string.Empty;
}