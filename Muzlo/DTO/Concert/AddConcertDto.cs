public class AddConcertDto : IMapTo<Concert>
{
    public DateTime Date {get; set;}
    public long HallId {get; set;}

    public string Name {get; set;} = string.Empty;
}