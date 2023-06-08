public class Concert 
{

    public long Id {get; set;}
    public DateTime Date {get; set;}
    public Hall? Hall {get; set;}

    public long? HallId {get; set;}

    public string Name {get; set;} = string.Empty;

    public List<Ticket> Tickets {get; set;} = new();

    public List<Performance>? Performances {get;set;}

    public ConcertState State {get; set;} = ConcertState.RELEVANT;
}