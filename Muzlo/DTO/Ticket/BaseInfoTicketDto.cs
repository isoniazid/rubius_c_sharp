public class BaseInfoTicketDto : IMapFrom<Ticket>
{
    public long Id {get; set;}
    public string? Hash {get; set;}
    public TicketConcertDto Concert {get; set;} = new();
    public TicketState State {get; set;}
}