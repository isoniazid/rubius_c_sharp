public interface IConcertService
{
    public Task AddNewConcert(AddConcertDto dto);

    public Task<List<BaseInfoTicketDto>> GetAllTickets(long id);
    public Task<List<BaseInfoTicketDto>> GetSoldTickets(long id);

    public Task<List<BaseInfoTicketDto>> GetUnsoldTickets(long id);

    public Task<BaseInfoConcertDto> GetBaseInfo(long id);

    public Task AddBand(AddBandPerformanceDto dto);

    public Task<BaseInfoConcertDto> CancelConcert(long id);

    public Task<List<BaseInfoConcertDto>> GetAll();
}