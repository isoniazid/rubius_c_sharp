public class TicketConcertDto : IMapFrom<Concert>
{
    public long Id {get; set;}
    public DateTime Date {get; set;}
    public string Name {get; set;} = string.Empty;
}