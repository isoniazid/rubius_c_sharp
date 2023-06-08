[ApiController]
[Route("api/hall")]

public class HallController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IHallService _service;
    public HallController(ApplicationDbContext context, IMapper mapper, IHallService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

    [HttpPost, Route("add_new_hall")]
    public async Task AddNewHall([FromBody] AddHallDto dto)
    {
        await _service.AddNewHall(dto);
    }

    [HttpGet, Route("get_all")]
    public async Task<List<BaseInfoHallDto>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet, Route("get_base_info")]
    public async Task<BaseInfoHallDto> GetBaseInfo([FromQuery] long id)
    {
        return await _service.GetBaseInfo(id);
    }

    [HttpGet, Route("get_concerts")]
    public async Task<List<GetAllConcertsFromHallDto>> GetConcerts([FromQuery] long id)
    {
        return await _service.GetConcerts(id);
    }

    [HttpDelete, Route("delete_Hall")]
    public async Task<BaseInfoHallDto> DeleteHall([FromQuery] long id)
    {
        return await _service.DeleteHall(id);
    }
}