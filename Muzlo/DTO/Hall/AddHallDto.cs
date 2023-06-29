public class AddHallDto : IMapTo<Hall>
{
    public long MaxViewers {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Address {get; set;} = string.Empty;
}