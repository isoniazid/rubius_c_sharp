[ApiController]
[Route("api/concert")]

public class ConcertController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IConcertService _service;
    public ConcertController(ApplicationDbContext context, IMapper mapper, IConcertService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

        [HttpPost, Route("add_new_concert")]
        public async Task AddNewConcert([FromBody] AddConcertDto dto)
        {
            await _service.AddNewConcert(dto);
        }

    [HttpGet, Route("get_all_tickets")]
    public async Task<List<BaseInfoTicketDto>> GetAllTickets([FromQuery] long id)
    {
        return await _service.GetAllTickets(id);
    }

    [HttpGet, Route("get_sold_tickets")]
    public async Task<List<BaseInfoTicketDto>> GetSoldTickets([FromQuery] long id)
    {
        return await _service.GetSoldTickets(id);
    }

    [HttpGet, Route("get_unsold_tickets")]
    public async Task<List<BaseInfoTicketDto>> GetUnsoldTickets([FromQuery] long id)
    {
        return await _service.GetUnsoldTickets(id);
    }

    [HttpGet, Route("get_base_info")]
    public async Task<BaseInfoConcertDto> GetBaseInfo([FromQuery] long id)
    {
        return await _service.GetBaseInfo(id);
    }

    [HttpGet, Route("get_all")]
    public async Task<List<BaseInfoConcertDto>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpPost, Route("add_band")]
    public async Task AddBand([FromBody] AddBandPerformanceDto dto)
    {
        await _service.AddBand(dto);
    }

    [HttpPatch, Route("cancel_concert")]
    public async Task<BaseInfoConcertDto> CancelConcert([FromQuery] long id)
    {
        return await _service.CancelConcert(id);
    }
}


