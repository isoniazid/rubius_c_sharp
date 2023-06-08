[ApiController]
[Route("api/band")]

public class BandController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IBandService _service;
    public BandController(ApplicationDbContext context, IMapper mapper, IBandService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

    [HttpPost, Route("add_new_band")]
    public async Task AddNewBand([FromBody] AddBandDto dto)
    {
        await _service.AddNewBand(dto);
    }

    [HttpGet, Route("get_all")]
    public async  Task<List<BaseInfoBandDto>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet, Route("get_base_info")]
    public async  Task<BaseInfoBandDto> GetBaseInfo([FromQuery] long id)
    {
        return await _service.GetBaseInfo(id);
    }

    [HttpPost, Route("add_member")]
    public async Task AddMember([FromBody] AddBandMemberDto dto)
    {
        await _service.AddMember(dto);
    }

    [HttpDelete, Route("delete_member")]
    public async Task<BandMemberDto> DeleteMember([FromQuery] long id)
    {
        return await _service.DeleteMember(id);
    }

}