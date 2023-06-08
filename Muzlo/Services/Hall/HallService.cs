public class HallService : IHallService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;
     public HallService(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task AddNewHall(AddHallDto dto)
    {
        var validation = Validate(dto);
        if(validation.state == ValidationResult.Fail) throw new APIException(validation.message, 400);


        var hallToSave = _mapper.Map<Hall>(dto);
        await _context.Halls.AddAsync(hallToSave);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BaseInfoHallDto>> GetAll()
    {
        return await _context.Halls.ProjectTo<BaseInfoHallDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<BaseInfoHallDto> GetBaseInfo(long id)
    {
        var raw_result = await _context.Halls.FindAsync(id) ?? throw new APIException($"Зал {id} не найден", 404);
        var result = _mapper.Map<BaseInfoHallDto>(raw_result);
        return result;
    }

    public async Task<List<GetAllConcertsFromHallDto>> GetConcerts(long id)
    {
        var raw_result = await  _context.Concerts.Where(p => p.HallId == id).ToListAsync();

        if(raw_result.Count == 0) throw new APIException($"Не найдены концерты для зала с id {id}",404); 

        var result = _mapper.Map<List<GetAllConcertsFromHallDto>>(raw_result); //NB
        return result;
    }

    public async Task<BaseInfoHallDto> DeleteHall(long id)
    {
        var hallToDelete = await _context.Halls.FindAsync(id) ?? throw new APIException($"Не найден зал с id {id}",404);

        var concertsOfHall = await _context.Concerts.Where(p => p.HallId == id).ToListAsync();

        if(concertsOfHall != null)
        {
            foreach(var currentConcert in concertsOfHall)
            {
                currentConcert.State = ConcertState.CANCELLED;
                var TicketsOfConcert = await _context.Tickets.Where(p => p.ConcertId == currentConcert.Id).ToListAsync();
                foreach(var currentTicket in TicketsOfConcert)
                {
                    currentTicket.State = TicketState.REFUND;
                }
            }
        }

        _context.Halls.Remove(hallToDelete);
        await _context.SaveChangesAsync();
        return _mapper.Map<BaseInfoHallDto>(hallToDelete);
    }


    private ValidationResult Validate(AddHallDto dto)
    {
        var result = new ValidationResult() {state = ValidationResult.Fail};
        if(_context.Halls.FirstOrDefault(p => p.Name == dto.Name) != null) result.message += "Зал с таким названием уже существует; ";
        if(dto.MaxViewers<=0) result.message += "Вместимость указана неверно; ";
        if(dto.MaxViewers > 500000) result.message += "Вместимость указана неверно: слишком большая; ";
        if(dto.Name == string.Empty) result.message += "Укажите название зала; ";
        if(dto.Address == string.Empty) result.message += "Укажите адрес; ";
        if(result.message == string.Empty) result.state = ValidationResult.Ok;

        return result;
    }
}