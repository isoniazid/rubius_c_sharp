public class Ticket 
{
    public long Id {get; set;}
    public string? Hash {get;set;}

    public long? ViewerId {get; set;}
    public Viewer? Viewer {get; set;}

    public long ConcertId {get; set;}
    public Concert? Concert {get; set;}

    public TicketState State {get; set;} = TicketState.RELEVANT;

    public void CreateHash()
    {
        var encrypter = new System.Security.Cryptography.HMACSHA256();
        encrypter.Key = Starter.ticketHashKey;
        Hash = Convert.ToBase64String(encrypter.ComputeHash(Encoding.UTF8.GetBytes(ViewerId.ToString()+ConcertId.ToString()+Id.ToString()))) 
        ?? throw new Exception("Incorrect values for Ticket CreateHash");
    }

}