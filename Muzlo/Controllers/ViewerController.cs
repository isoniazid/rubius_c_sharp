[ApiController]
[Route("api/viewer")]

public class ViewerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IViewerService _service;
    public ViewerController(ApplicationDbContext context, IMapper mapper, IViewerService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

    [HttpPost, Route("add_viewer")]
    public async Task AddViewer([FromBody]AddViewerDto dto)
    {
        await _service.AddViewer(dto);
    }

    [HttpPatch, Route("buy_ticket")]
    public async Task BuyTicket([FromBody]BuyTicketDto dto)
    {
        await _service.BuyTicket(dto);
    }

    [HttpGet, Route("get_all_tickets")]
    public  async Task<List<BaseInfoTicketDto>> GetAllTickets([FromQuery]long viewerId)
    {
        return await _service.GetAllTickets(viewerId);
    }

}