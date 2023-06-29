public interface IViewerService
{
    public  Task AddViewer(AddViewerDto dto);

    public  Task BuyTicket(BuyTicketDto dto);
    public  Task<List<BaseInfoTicketDto>> GetAllTickets(long viewerId);
}