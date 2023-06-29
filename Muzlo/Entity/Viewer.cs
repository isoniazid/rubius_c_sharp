public class Viewer
{
    public long Id {get; set;}
    public string Name {get; set;} = "Аноним";

    public List<Ticket>? Tickets {get; set;}
}